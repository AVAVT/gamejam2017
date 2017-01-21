using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class PlayerController : MonoBehaviour
{
	public static PlayerController Instance { get; private set; }

	public Animator mainAnimator;

	public PlayerAnimationSet defaultPASet;
	public PlayerAnimationSet leftPASet;
	public PlayerAnimationSet rightPASet;

	public Vector3 acceleration;
	public Vector3 bocDauSpeed;
	public Vector3 bocDauDrag;
	public Vector3 deccelerateMaxSpeed;
	public float maxSpeed;
	public float arrivalZoneRadius;

	public float skill1CooldownTime;

	private SpriteRenderer sr;
    private Rigidbody2D rib;
	private bool allowControl = true;
	private bool arriving = false;
	private bool alive = true;
	private float initialY;


    void Awake() {
		Instance = this;
        rib = GetComponent<Rigidbody2D>();
		sr = GetComponent<SpriteRenderer> ();
    }

	void Start(){
		initialY = transform.position.y;
	}

	void Update(){
		if (!alive)
			return;
		
		if (rib.velocity.x < -30) {
			if (sr.sprite != leftPASet.bikeSprite) {
				SwitchAnimationSet (leftPASet);
			}
		} else if (rib.velocity.x > 30) {
			if (sr.sprite != rightPASet.bikeSprite) {
				SwitchAnimationSet (rightPASet);
			}
		} else if (sr.sprite != defaultPASet.bikeSprite) {
			SwitchAnimationSet (defaultPASet);
		}
	}

	void FixedUpdate ()
	{
		if (!alive)
			return;
		
		if (allowControl) {
			float moveHorizontal = Input.GetAxis ("Horizontal");

			Vector3 direction = new Vector3 (moveHorizontal, 0, 0);
			direction.Scale (acceleration);
			if (rib.velocity.x == 0 || direction.x / rib.velocity.x < 0
			    || (rib.velocity.x / maxSpeed > -1 && rib.velocity.x / maxSpeed < 1)) {

				rib.AddForce (direction);
			}
		} else if (!arriving) {
			if (rib.velocity.y > deccelerateMaxSpeed.y) {
				Debug.Log ("aaaa");
				rib.AddForce (bocDauDrag);
			}

			if (rib.velocity.y < 0 && transform.position.y <= initialY + arrivalZoneRadius) {
				arriving = true;
			}
		} else if (rib.velocity.y < 0 && initialY+2 < transform.position.y) {			
			rib.velocity = Vector3.Lerp (Vector3.zero, deccelerateMaxSpeed, Mathfx.Sinerp(0,1,Mathf.Clamp((transform.position.y-initialY) / arrivalZoneRadius, 0, 1)));
			Debug.Log(Mathf.Clamp ((transform.position.y - initialY) / arrivalZoneRadius, 0, 1));
		} else{
			arriving = false;
			allowControl = true;
			rib.velocity = Vector3.zero;
			transform.position = new Vector3 (transform.position.x, initialY, transform.position.z);
		}
	}

	public void BocDau(){
		if (allowControl) {
			allowControl = false;

			rib.velocity = bocDauSpeed;
		}
	}
	void OnTriggerEnter2D(Collider2D other){
		if (other.gameObject.tag == "EnemyHead" || !allowControl) {
			other.transform.parent.GetComponent<Enemy> ().Die (rib.velocity.x > 0 ? -1 : 1);
		}
		else if (other.gameObject.tag == "EnemyBack") {
			Die (rib.velocity.x > 0 ? -1 : 1);
		}
	}


	void Die(int direction){
		alive = false;
		transform.Rotate (new Vector3 (0, 0, direction * 32));
		List<BoxCollider2D> colliders = new List<BoxCollider2D>();
		GetComponents<BoxCollider2D> (colliders);
		for (int i = 0; i < colliders.Count; i++) {
			colliders [i].enabled = false;
		}
		mainAnimator.speed = 0;
		StartCoroutine (AnimateDead());
	}

	IEnumerator AnimateDead(){
		Vector3 startVelocity = new Vector3 (rib.velocity.x*0.7f, BackgroundScroller.Instance.scrollSpeed*0.5f, 0);
		Vector3 endVelocity = new Vector3 (rib.velocity.x*0.3f, BackgroundScroller.Instance.scrollSpeed, 0);

		float time = 0;
		while (time < 0.3f) {
			rib.velocity = Vector3.Lerp (startVelocity, endVelocity, Mathfx.Sinerp (0, 1, time / 0.3f));

			time += Time.deltaTime;
			yield return null;
		}

		GamePlayScene.Instance.GameOver ();
	}

	private void SwitchAnimationSet(PlayerAnimationSet set){
		sr.sprite = set.bikeSprite;
		mainAnimator.runtimeAnimatorController = set.mainAnimator;
		mainAnimator.gameObject.transform.localPosition = set.mainPos;
	}
}

[System.Serializable]
public class PlayerAnimationSet{
	public Sprite bikeSprite;
	public Sprite mainSprite;
	public Vector2 mainPos;
	public RuntimeAnimatorController mainAnimator;
}
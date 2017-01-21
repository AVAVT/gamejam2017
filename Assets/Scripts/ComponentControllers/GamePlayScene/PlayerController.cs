using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class PlayerController : MonoBehaviour
{
	public static PlayerController Instance { get; private set; }

	public GameObject allyLeftPrefab;
	public GameObject allyRightPrefab;

	public Animator mainAnimator;
	public GameObject exhaust;

	public PlayerAnimationSet defaultPASet;
	public PlayerAnimationSet leftPASet;
	public PlayerAnimationSet rightPASet;
	public PlayerAnimationSet bocdauPASet;

	public Vector3 acceleration;
	public Vector3 bocDauSpeed;
	public Vector3 bocDauDrag;
	public Vector3 deccelerateMaxSpeed;
	public float maxSpeed;
	public float arrivalZoneRadius;

	public float skill1CooldownTime;
    public float skill2CooldownTime;

	private SpriteRenderer sr;
	public Rigidbody2D rib { get; private set;}
	private bool allowControl = true;
	private bool arriving = false;
	public bool alive { get; private set; }
	private float initialY;

    public GameObject netBoObject;
	public bool isNetBo{get; private set;}
    public float netBoTime = 3;
	public float netBoAoeRange;

    void Awake() {
		Instance = this;
        rib = GetComponent<Rigidbody2D>();
		sr = GetComponent<SpriteRenderer> ();
		isNetBo = false;
		alive = true;
    }

	void Start(){
		initialY = transform.position.y;
	}

	void Update(){
		if (!alive)
			return;

		if (allowControl) {
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
				rib.AddForce (bocDauDrag);
			}

			if (rib.velocity.y < 0 && transform.position.y <= initialY + arrivalZoneRadius) {
				arriving = true;
				SwitchAnimationSet (defaultPASet);
			}
		} else if (rib.velocity.y < 0 && initialY+2 < transform.position.y) {			
			rib.velocity = Vector3.Lerp (Vector3.zero, deccelerateMaxSpeed, Mathfx.Sinerp(0,1,Mathf.Clamp((transform.position.y-initialY) / arrivalZoneRadius, 0, 1)));
		} else{
			arriving = false;
			allowControl = true;
			rib.velocity = Vector3.zero;
			transform.position = new Vector3 (transform.position.x, initialY, transform.position.z);
		}
	}

	public void GoiDoi(){
		TKUtils.Instantiate (allyLeftPrefab, new Vector3(transform.position.x + 300, - 540, 0), Quaternion.identity);
		TKUtils.Instantiate (allyLeftPrefab, new Vector3(transform.position.x + 450, - 540, 0), Quaternion.identity);
		TKUtils.Instantiate (allyRightPrefab, new Vector3(transform.position.x - 300, - 540, 0), Quaternion.identity);
		TKUtils.Instantiate (allyRightPrefab, new Vector3(transform.position.x - 450, - 540, 0), Quaternion.identity);
	}

	public void BocDau(){
		if (allowControl) {
			allowControl = false;
			SwitchAnimationSet (bocdauPASet);
			rib.velocity = bocDauSpeed;
			ExhaustController.Instance.NetBoFire ();
		}
	}

    public IEnumerator INetBo()
    {
		ExhaustController.Instance.NetBoFire ();
		isNetBo = true;
		EnemyPool.Instance.CheckNetBo ();
        netBoObject.SetActive(true);
        yield return new WaitForSeconds(netBoTime);
        netBoObject.SetActive(false);
		isNetBo = false;
    }

	void OnTriggerEnter2D(Collider2D other){
		if (other.gameObject.tag == "Ally")
			return;
		
		if (other.gameObject.tag == "EnemyHead" || !allowControl) {
			if (other.transform.parent != null) {
				other.transform.parent.GetComponent<Enemy> ().Die (rib.velocity.x > 0 ? -1 : 1);	
			} else {
				Destroy (other.gameObject);
			}
		}
		else if (other.gameObject.tag == "EnemyBack" || other.gameObject.tag == "Obstacle" || other.gameObject.tag == "Arrow") {
			Die (rib.velocity.x > 0 ? 1 : -1);
			if (other.gameObject.tag == "Arrow") {
				other.GetComponent<ArrowController> ().OnHit ();
			}
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
		sr.sprite = direction > 0 ? rightPASet.bikeCrash : leftPASet.bikeCrash;
		mainAnimator.runtimeAnimatorController = direction > 0 ? rightPASet.dieAnimator : leftPASet.dieAnimator;
		StartCoroutine (AnimateDead(direction ));
	}

	IEnumerator AnimateDead(int direction){
		Vector3 startVelocity = new Vector3 (-rib.velocity.x*0.7f, BackgroundScroller.Instance.scrollSpeed*0.5f, 0);
		Vector3 endVelocity = new Vector3 (-rib.velocity.x*0.3f, BackgroundScroller.Instance.scrollSpeed, 0);
		Vector3 mainVelocity = (Vector3.right + Vector3.up*Random.Range(0.0f, 0.3f)) * rib.velocity.x;
		float time = 0;
		while (time < 0.3f) {
			rib.velocity = Vector3.Lerp (startVelocity, endVelocity, Mathfx.Sinerp (0, 1, time / 0.3f));
			mainAnimator.transform.localPosition += mainVelocity * Time.deltaTime;

			time += Time.deltaTime;
			yield return null;
		}

		GamePlayScene.Instance.GameOver ();
	}

	private void SwitchAnimationSet(PlayerAnimationSet set){
		sr.sprite = set.bikeSprite;
		mainAnimator.runtimeAnimatorController = set.mainAnimator;
		mainAnimator.gameObject.transform.localPosition = set.mainPos;
		exhaust.transform.localPosition = set.exhaustPos;
	}
}

[System.Serializable]
public class PlayerAnimationSet{
	public Sprite bikeSprite;
	public Sprite mainSprite;
	public Sprite bikeCrash;
	public Vector2 mainPos;
	public Vector2 exhaustPos;
	public RuntimeAnimatorController mainAnimator;
	public RuntimeAnimatorController dieAnimator;
}
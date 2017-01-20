using UnityEngine;
using System.Collections;

[System.Serializable]
public class PlayerController : MonoBehaviour
{
	public static PlayerController Instance { get; private set; }

	public Animator mainAnimator;
	public RuntimeAnimatorController animatorControllerDefault;
	public RuntimeAnimatorController animatorControllerLeft;
	public RuntimeAnimatorController animatorControllerRight;
	public Sprite leftSprite;
	public Sprite rightSprite;
	public Sprite defaultSprite;
	public Vector2 leftMainPos;
	public Vector2 rightMainPos;
	public Vector2 defaultMainPos;

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
		if (rib.velocity.x < -30) {
			if(sr.sprite != leftSprite){
				sr.sprite = leftSprite;
				mainAnimator.runtimeAnimatorController = animatorControllerLeft;
				mainAnimator.gameObject.transform.localPosition = leftMainPos;
			}
		} else if (rib.velocity.x > 30) {
			if (sr.sprite != rightSprite) {
				sr.sprite = rightSprite;
				mainAnimator.runtimeAnimatorController = animatorControllerRight;
				mainAnimator.gameObject.transform.localPosition = rightMainPos;
			}
		} else if(sr.sprite != defaultSprite) {
			sr.sprite = defaultSprite;
			mainAnimator.runtimeAnimatorController = animatorControllerDefault;
			mainAnimator.gameObject.transform.localPosition = defaultMainPos;
		}
	}

	void FixedUpdate ()
	{
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
			other.transform.parent.gameObject.SetActive (false);
		}
		else if (other.gameObject.tag == "EnemyBack") {
			GamePlayScene.Instance.GameOver ();
		}
	}
}

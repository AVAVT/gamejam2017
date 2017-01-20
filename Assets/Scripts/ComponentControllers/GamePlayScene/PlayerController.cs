using UnityEngine;
using System.Collections;

[System.Serializable]
public class PlayerController : MonoBehaviour
{
	public static PlayerController Instance { get; private set; }

	public Vector3 acceleration;
	public Vector3 bocDauSpeed;
	public Vector3 bocDauDrag;
	public Vector3 deccelerateMaxSpeed;
	public float maxSpeed;
	public float arrivalZoneRadius;

	public float skill1CooldownTime;

    private Rigidbody2D rib;
	private bool allowControl = true;
	private bool arriving = false;
	private float initialY;


    void Awake() {
		Instance = this;
        rib = GetComponent<Rigidbody2D>();
    }

	void Start(){
		initialY = transform.position.y;
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
			Debug.Log (rib.velocity.y);
			Debug.Log (deccelerateMaxSpeed.y);
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
}

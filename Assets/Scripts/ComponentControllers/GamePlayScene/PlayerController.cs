using UnityEngine;
using System.Collections;

[System.Serializable]
public class PlayerController : MonoBehaviour
{
	public Vector3 acceleration;
	public float maxSpeed;

    private Rigidbody rib;

    void Awake() {
        rib = GetComponent<Rigidbody>();
    }

	void FixedUpdate ()
	{
		float moveHorizontal = Input.GetAxis ("Horizontal");

		Vector3 direction = new Vector3 (moveHorizontal, 0, 0);
		direction.Scale (acceleration);
		if (direction.x / rib.velocity.x < 0 
			|| (rib.velocity.x / maxSpeed > -1 && rib.velocity.x / maxSpeed < 1)) {

			rib.AddForce (direction);
		}
	}
}

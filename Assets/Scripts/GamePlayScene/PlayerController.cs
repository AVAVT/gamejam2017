using UnityEngine;
using System.Collections;

[System.Serializable]
public class PlayerController : MonoBehaviour
{
	public float speed;

    private Rigidbody rib;

    void Awake() {
        rib = GetComponent<Rigidbody>();
    }

	void FixedUpdate ()
	{
		float moveHorizontal = Input.GetAxis ("Horizontal");
		float moveVertical = Input.GetAxis ("Vertical");

		Vector3 movement = new Vector3 (moveHorizontal, moveVertical, 0);
        rib.velocity = movement * speed;
	}
}

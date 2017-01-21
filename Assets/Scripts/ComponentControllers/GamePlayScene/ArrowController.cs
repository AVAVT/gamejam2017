using UnityEngine;
using System.Collections;

public class ArrowController : MonoBehaviour {
	private static float ARROW_SPEED = 700;

	private Rigidbody2D rib;
	private SpriteRenderer sr; 

	void Awake(){
		rib = GetComponent<Rigidbody2D> ();
		sr = GetComponent<SpriteRenderer> ();
	}

	void OnEnable(){
		Vector3 direction = ((PlayerController.Instance.transform.position + (Vector3)PlayerController.Instance.rib.velocity) - transform.position).normalized;
		if (Mathf.Abs(direction.x / direction.y) > 0.2f) {
			
			direction = Vector3.down;
		}
		rib.velocity = direction * ARROW_SPEED;
	}

	public void OnHit(){
		float angle = Random.Range (0, Mathf.PI);
		rib.velocity = new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0) * 400;
		rib.angularVelocity = Random.Range (400.0f, 900.0f) * (Random.Range (0, 2) * 2 - 1);
	}

	IEnumerator AnimateDestroy(){
		float time = 0;
		while (time < 0.4f) {
			sr.color = Color.Lerp (Color.white, Color.clear, Mathfx.Sinerp (0, 1, time / 0.4f));

			time += Time.deltaTime;
			yield return null;
		}

		Destroy(gameObject);
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.tag.Equals("Bottom"))
		{
			Destroy(gameObject);
		}
	}
}

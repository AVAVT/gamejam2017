using UnityEngine;
using System.Collections;

public class AllyController : MonoBehaviour {
	public bool isAllyLeft;

	private Rigidbody2D rib;

	void Awake(){
		rib = GetComponent<Rigidbody2D> ();
	}

	void Start () {
		if (isAllyLeft) {
			rib.velocity = new Vector3 (-4.5f, 10, 0).normalized * 600; 
		} else {
			rib.velocity = new Vector3 (4.5f, 10, 0).normalized * 600; 
		}
	}

	void Update(){
		if (transform.position.y > 600) {
			Destroy (gameObject);
		}
	}

	void OnTriggerEnter2D(Collider2D other){
		if (other.gameObject.tag == "EnemyHead" || other.gameObject.tag == "EnemyBack" || other.gameObject.tag == "Arrow") {
			if (other.transform.parent != null) {
				AudioManager.Instance.PlayEnemyDeadSound ();
				other.transform.parent.GetComponent<Enemy> ().Die (rib.velocity.x > 0 ? -1 : 1);	
			} else {
				Destroy (other.gameObject);
			}
		}
	}

}

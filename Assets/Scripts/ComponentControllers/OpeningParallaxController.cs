using UnityEngine;
using System.Collections;

public class OpeningParallaxController : MonoBehaviour {

	public Vector3 speed;
	public float loopSize;

	void Update(){
		transform.position += speed * Time.deltaTime;
		if (transform.position.x < -loopSize) {
			transform.position = transform.position + new Vector3 (loopSize, 0, 0);
		}
	}
}

using UnityEngine;
using System.Collections;

public class OpeningLayerDoanDuaController : MonoBehaviour {

	void Start () {
		StartCoroutine (MoveIn ());
	}
	
	IEnumerator MoveIn(){
		Vector3 startPos = new Vector3 (-700, 0, 0);
		float time = 0;
		while (time < 10) {
			transform.localPosition = Vector3.Lerp (startPos, Vector3.zero, Mathfx.Sinerp (0, 1, time / 10f));

			time += Time.deltaTime;
			yield return null;
		}

		transform.localPosition = Vector3.zero;
	}
}

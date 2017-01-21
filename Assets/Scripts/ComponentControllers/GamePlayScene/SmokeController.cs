using UnityEngine;
using System.Collections;

public class SmokeController : MonoBehaviour {

	private SpriteRenderer sr;

	void Awake(){
		sr = GetComponent<SpriteRenderer> ();	
	}
	void OnEnable(){
		StartCoroutine (Trailing());
	}
	IEnumerator Trailing(){
		float time = 0;
		Color clear = Color.white;
		clear.a = 0;
		while (time < 4.0f) {
			transform.position += (Vector3.up * BackgroundScroller.Instance.scrollSpeed) * Time.deltaTime;
			sr.color = Color.Lerp (Color.white, clear, Mathfx.Sinerp(0, 1, time / 4f));

			time += Time.deltaTime;
			yield return null;
		}

		gameObject.SetActive (false);
		sr.color = Color.white;
	}
}

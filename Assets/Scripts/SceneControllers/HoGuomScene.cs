using UnityEngine;
using System.Collections;

public class HoGuomScene : MonoBehaviour {

	void Start () {
		AudioManager.Instance.PlayHoGuomBGM ();
		StartCoroutine (WaitForSceneChange ());
	}

	IEnumerator WaitForSceneChange(){
		yield return new WaitForSeconds (12.3f);
		AudioManager.Instance.StopHoGuomBGM ();
		TKSceneManager.ChangeScene (TKSceneManager.OPENING_2);
	}
}

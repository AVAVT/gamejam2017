using UnityEngine;
using System.Collections;

public class HoGuomScene : MonoBehaviour {

	// Use this for initialization
	void Start () {
		AudioManager.Instance.PlayHoGuomBGM ();
		StartCoroutine (WaitForSceneChange ());
	}

	IEnumerator WaitForSceneChange(){
		yield return new WaitForSeconds (12.3f);
		AudioManager.Instance.StopHoGuomBGM ();
		TKSceneManager.ChangeScene (TKSceneManager.GAME_PLAY_SCENE);
	}
}

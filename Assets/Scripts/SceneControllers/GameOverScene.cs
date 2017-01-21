using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverScene : MonoBehaviour {

	public void Update(){
		if (Input.GetKeyDown (KeyCode.Space)) {
			Replay ();
		}
	}

    public void Replay()
    {
		TKSceneManager.ChangeScene(TKSceneManager.GAME_PLAY_SCENE);
		gameObject.SetActive (false);
    }
}

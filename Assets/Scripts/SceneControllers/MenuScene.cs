using UnityEngine;
using System.Collections;

public class MenuScene : MonoBehaviour {

	public void StartGame(){
		TKSceneManager.ChangeScene (TKSceneManager.GAME_PLAY_SCENE);
        Debug.Log("Start");
	}
}

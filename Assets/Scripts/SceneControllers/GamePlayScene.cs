﻿using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GamePlayScene : MonoBehaviour {

	void Start(){
		SceneManager.LoadScene (TKSceneManager.HUD_GAME_PLAY_SCENE, LoadSceneMode.Additive);
	}

	public void GameOver(){
		TKSceneManager.ChangeScene (TKSceneManager.GAME_OVER_SCENE);
	}

	public void Victory(){
		TKSceneManager.ChangeScene (TKSceneManager.VICTORY_SCENE);
	}
}

using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GamePlayScene : MonoBehaviour {
	public static GamePlayScene Instance {get; private set;}

	void Awake(){
		Instance = this;
	}

	void Start(){
		SceneManager.LoadScene (TKSceneManager.HUD_GAME_PLAY_SCENE, LoadSceneMode.Additive);
	}

	public void GameOver(){
		TKSceneManager.ChangeScene (TKSceneManager.GAME_OVER_SCENE);
	}

	public void Victory(){
		StartCoroutine (AnimateVictory ());
	}

	IEnumerator AnimateVictory(){
		yield return new WaitForSeconds (13);
		TKSceneManager.ChangeScene (TKSceneManager.VICTORY_SCENE);
	}
}

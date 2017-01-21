using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverScene : MonoBehaviour {

	public List<GameObject> videoPlayers;
	public GameObject notification;

	void Start(){
		GameObject vd = videoPlayers [Random.Range (0, videoPlayers.Count)];
		vd.SetActive (true);
		StartCoroutine (AnimateNotification(vd.GetComponent<VideoPlayer> ().movie.duration));
	}

	void Update(){
		if (Input.GetKeyDown (KeyCode.Space)) {
			Replay ();
		}
	}

	IEnumerator AnimateNotification(float duration){
		yield return new WaitForSeconds (duration);
		notification.SetActive (true);
	}

    public void Replay()
    {
		TKSceneManager.ChangeScene(TKSceneManager.GAME_PLAY_SCENE);
		gameObject.SetActive (false);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverScene : MonoBehaviour {

    public void Replay()
    {
		TKSceneManager.ChangeScene(TKSceneManager.GAME_PLAY_SCENE);
    }
}

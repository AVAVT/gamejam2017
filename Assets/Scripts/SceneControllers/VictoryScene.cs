using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VictoryScene : MonoBehaviour {

    public void GotoVictory()
    {
        TKSceneManager.ChangeScene(TKSceneManager.VICTORY_SCENE);
    }
    public void GotoMenu()
    {
        TKSceneManager.ChangeScene(TKSceneManager.MENU_SCENE);

    }
}

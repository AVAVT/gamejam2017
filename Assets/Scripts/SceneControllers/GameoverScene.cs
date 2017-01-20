using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameoverScene : MonoBehaviour {

    public void gotoMenu()
    {
        TKSceneManager.ChangeScene(TKSceneManager.MENU_SCENE);
        Debug.Log("Start");
    }
    public void gotoGameover()
    {
        TKSceneManager.ChangeScene(TKSceneManager.GAMEOVER_SCENE);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VictoryScript : MonoBehaviour {

    public void gotoVictory()
    {
        TKSceneManager.ChangeScene(TKSceneManager.VICTORY_SCENE);
    }
    public void gotoMenu()
    {
        TKSceneManager.ChangeScene(TKSceneManager.MENU_SCENE);

    }
}

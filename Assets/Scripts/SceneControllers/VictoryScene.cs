using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VictoryScene : MonoBehaviour {

	void Start()
    {
		Invoke ("GoToMenu", 1);
    }

	void GoToMenu(){
		TKSceneManager.ChangeScene(TKSceneManager.MENU_SCENE);
	}
    
}

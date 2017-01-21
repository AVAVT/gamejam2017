using UnityEngine;
using System.Collections;

public class MenuScene : MonoBehaviour {

	public void StartGame(){
		TKSceneManager.ChangeScene (TKSceneManager.OPENING_1);
	}
}

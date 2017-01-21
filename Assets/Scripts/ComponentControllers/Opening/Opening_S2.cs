using UnityEngine;
using System.Collections;

public class Opening_S2 : MonoBehaviour {

	void Awake(){
		StartCoroutine (ChangeScene());
	}

	IEnumerator ChangeScene()
    {
		yield return new WaitForSeconds (2);
        TKSceneManager.ChangeScene(TKSceneManager.OPENING_3);
    }
}

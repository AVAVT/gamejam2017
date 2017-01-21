using UnityEngine;
using System.Collections;

public class Opening_S4 : MonoBehaviour {
    public Transform meGiong;

	void Awake(){
		StartCoroutine (ChangeScene());
	}

	IEnumerator ChangeScene()
	{
		yield return new WaitForSeconds (5);
		TKSceneManager.ChangeScene(TKSceneManager.OPENING_5);
	}

    void Update()
    {
        meGiong.transform.Translate(new Vector3(200*Time.deltaTime, 0,0));
    }
}

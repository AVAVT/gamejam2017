using UnityEngine;
using System.Collections;

public class Opening_S5 : MonoBehaviour {


    public AnimationClip clip;

    private float timeDuring = 0;
    
	void Start()
    {
		timeDuring = clip.length;
		StartCoroutine (ChangeScene());
    }

	IEnumerator ChangeScene()
	{
		yield return new WaitForSeconds (timeDuring + 0.3f);
		TKSceneManager.ChangeScene(TKSceneManager.GAME_PLAY_SCENE);
	}
}

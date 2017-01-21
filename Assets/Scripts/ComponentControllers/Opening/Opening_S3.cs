using UnityEngine;
using System.Collections;

public class Opening_S3 : MonoBehaviour {

    void Update()
    {
        if (Time.time < 5)
        {
            TKSceneManager.ChangeScene(TKSceneManager.OPENING_3);
        }
    }
}

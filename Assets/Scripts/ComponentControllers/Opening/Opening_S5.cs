﻿using UnityEngine;
using System.Collections;

public class Opening_S5 : MonoBehaviour {


    public AnimationClip clip;

    private float timeDuring = 0;
    void Start()
    {
        timeDuring = clip.length;
    }
    void Update()
    {
        if (Time.time > timeDuring + 0.1f)
        {
            TKSceneManager.ChangeScene(TKSceneManager.GAME_PLAY_SCENE);
        }
    }
    public void EndAnimation()
    {

    }
}
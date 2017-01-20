using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class GameConfigs : MonoBehaviour
{
	public static GameConfigs Instance { get; private set; }

	void Awake ()
	{
		if (Instance == null) {
			Instance = this;
			DontDestroyOnLoad (this);
			Init ();
		}
	}

	private void Init ()
	{
		
    }
		
	#region GAME CONSTANTS


	public static readonly string PICKUP_TEXT_COIN = "KA-CHING";



	#endregion

	#region GAME SETTINGS


    #endregion
}



﻿using UnityEngine;
using System.Collections;

public class AudioManager : MonoBehaviour {
	public static AudioManager Instance { get; private set; }

	void Awake(){
		if (Instance == null) {
			Instance = this;
			DontDestroyOnLoad (gameObject);
		}
	}
		
	public AudioSource UIToggleSource;
	public AudioSource UIClickSource;
	public AudioSource HoGuomSource;

	public bool ToggleSound(){
		AudioListener.volume = AudioListener.volume > 0 ? 0 : 1;
		return (AudioListener.volume > 0);
	}

	public void PlayClickSound(){
		UIClickSource.Play ();
	}

	public void PlayToggleSound(){
		UIToggleSource.Play ();
	}

	public void PlayHoGuomBGM(){
		HoGuomSource.Play ();
	}

	public void StopHoGuomBGM(){
		HoGuomSource.Stop ();
	}
}

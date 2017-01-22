using UnityEngine;
using System.Collections;
using System.Collections.Generic;

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

	public AudioSource CoiSource;
	public AudioSource BocDauSource;
	public AudioSource BattleSource;

	public AudioSource WaveSource;

	public List<AudioSource> enemyDeadSource;
	public AudioSource mainDeadSource;

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

	public void PlayCoiSound(){
		CoiSource.Play ();
	}

	public void PlayBocDauSound(){
		BocDauSource.Play ();
	}

	public void PlayHoGuomBGM(){
		HoGuomSource.Play ();
	}

	public void StopHoGuomBGM(){
		HoGuomSource.Stop ();
	}

	public void PlayBattleSound(){
		BattleSource.Play ();
	}

	public void PlayEnemyDeadSound(){
		enemyDeadSource [Random.Range (0, enemyDeadSource.Count)].Play ();
	}

	public void PlayMainDeadSound(){
		mainDeadSource.Play ();
	}

	public void PlayWaveSound(){
		WaveSource.Play ();
	}

}

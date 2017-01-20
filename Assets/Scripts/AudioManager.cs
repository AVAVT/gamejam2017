using UnityEngine;
using System.Collections;

public class AudioManager : MonoBehaviour {
	public static AudioManager Instance { get; private set; }

	void Awake(){
		if (Instance == null) {
			Instance = this;
		}
	}
		
	public AudioSource UIToggleSource;
	public AudioSource UIClickSource;

	public void ToggleSound(){
		AudioListener.volume = AudioListener.volume > 0 ? 0 : 1;
	}

	public void PlayClickSound(){
		UIClickSource.Play ();
	}
	public void PlayToggleSound(){
		UIToggleSource.Play ();
	}

}

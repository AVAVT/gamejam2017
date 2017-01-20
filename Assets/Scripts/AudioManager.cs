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


	public void PlayClickSound(){
		UIClickSource.Play ();
	}
	public void PlayToggleSound(){
		UIToggleSource.Play ();
	}

}

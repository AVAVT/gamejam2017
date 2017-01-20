using UnityEngine;
using System.Collections;

public class HUDController : MonoBehaviour {
	
	void Start () {
		Time.timeScale = 0;
	}

	public void OnPauseButtonClicked(){
		Debug.Log ("OnPauseButtonClicked");
	}
	public void OnSoundButtonClicked(){
		Debug.Log ("OnSoundButtonClicked");
	}


	public void OnSkill1ButtonClicked(){
		Debug.Log ("OnSkill1ButtonClicked");
	}
	public void OnSkill2ButtonClicked(){
		Debug.Log ("OnSkill2ButtonClicked");
	}
	public void OnSkill3ButtonClicked(){
		Debug.Log ("OnSkill3ButtonClicked");
	}
	public void OnSkill4ButtonClicked(){
		Debug.Log ("OnSkill4ButtonClicked");
	}
	public void OnSkill5ButtonClicked(){
		Debug.Log ("OnSkill5ButtonClicked");
	}
}

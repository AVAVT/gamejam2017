using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HUDController : MonoBehaviour {

	public Canvas canvas;

	void Start () {
		canvas.worldCamera = Camera.main;
		canvas.sortingLayerName = "HUD";
	}

	public void OnPauseButtonClicked(){
		Time.timeScale = Time.timeScale > 0 ? 0 : 1;
	}
	public void OnSoundButtonClicked(){
		AudioManager.Instance.ToggleSound ();
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

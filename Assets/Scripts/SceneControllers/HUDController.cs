using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HUDController : MonoBehaviour {

	public Canvas canvas;

	public Button pauseButton;
	public Button soundButton;
	public Button skill1Button;
	public Button skill2Button;
	public Button skill3Button;
	public Button skill4Button;
	public Button skill5Button;

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
		PlayerController.Instance.BocDau ();
		skill1Button.interactable = false;
		StartCoroutine (CooldownSkill (skill1Button, PlayerController.Instance.skill1CooldownTime));
	}
	public void OnSkill2ButtonClicked(){
        StartCoroutine(PlayerController.Instance.INetBo());
        skill2Button.interactable = false;
        StartCoroutine(CooldownSkill(skill2Button, PlayerController.Instance.skill2CooldownTime));
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

	private IEnumerator CooldownSkill(Button skillButton, float time){
		yield return new WaitForSeconds (time);
		skillButton.interactable = true;
	}
}

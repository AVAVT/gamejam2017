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

	public GameObject skill1Panel;
	public GameObject skill2Panel;
	public GameObject skill3Panel;
	public GameObject skill4Panel;
	public GameObject skill5Panel;

	public Sprite soundOnSprite;
	public Sprite soundOffSprite;
	public Sprite playSprite;
	public Sprite pauseSprite;

	public GameObject gamePausedText;

	void Start () {
		canvas.worldCamera = Camera.main;
		canvas.sortingLayerName = "HUD";
	}

	void Update(){
		if (Input.GetKeyDown (KeyCode.Q)) {
			OnSkill1ButtonClicked ();
		} else if (Input.GetKeyDown (KeyCode.W)) {
			OnSkill2ButtonClicked ();
		} else if (Input.GetKeyDown (KeyCode.P)) {
			OnPauseButtonClicked ();
		}
	}

	public void OnPauseButtonClicked(){
		if (Time.timeScale > 0) {
			Time.timeScale = 0;
			pauseButton.GetComponent<Image> ().sprite = playSprite;
			gamePausedText.SetActive (true);
		} else {
			Time.timeScale = 1;
			pauseButton.GetComponent<Image> ().sprite = pauseSprite;
			gamePausedText.SetActive (false);
		}
	}

	public void OnSoundButtonClicked(){
		soundButton.GetComponent<Image>().sprite = AudioManager.Instance.ToggleSound () ? soundOnSprite : soundOffSprite;
	}

	public void OnSkill1ButtonClicked(){
		if (!skill1Button.interactable || !PlayerController.Instance.alive)
			return;
		
		PlayerController.Instance.BocDau ();
		skill1Button.interactable = false;
		StartCoroutine (CooldownSkill (skill1Button, PlayerController.Instance.skill1CooldownTime));
	}
	public void OnSkill2ButtonClicked(){
		if (!skill2Button.interactable || !PlayerController.Instance.alive)
			return;
		
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

	public void ShowSkillPanel(int skillNo){
		switch (skillNo) {
		case 1:
			skill1Panel.SetActive (true);
			break;
		case 2:
			skill2Panel.SetActive (true);
			break;
		case 3:
			skill3Panel.SetActive (true);
			break;
		case 4:
			skill4Panel.SetActive (true);
			break;
		case 5:
			skill5Panel.SetActive (true);
			break;
		default:
			break;
		}
	}
	public void HideSkillPanel(int skillNo){
		switch (skillNo) {
		case 1:
			skill1Panel.SetActive (false);
			break;
		case 2:
			skill2Panel.SetActive (false);
			break;
		case 3:
			skill3Panel.SetActive (false);
			break;
		case 4:
			skill4Panel.SetActive (false);
			break;
		case 5:
			skill5Panel.SetActive (false);
			break;
		default:
			break;
		}
	}

	private IEnumerator CooldownSkill(Button skillButton, float time){
		yield return new WaitForSeconds (time);
		skillButton.interactable = true;
	}
}

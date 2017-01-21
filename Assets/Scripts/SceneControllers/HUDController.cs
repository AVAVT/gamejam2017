using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class HUDController : MonoBehaviour {
	public static HUDController Instance { get; private set; }

	void Awake(){
		Instance = this;
	}

	public Canvas canvas;

	public Button pauseButton;
	public Button soundButton;
	public List<Button> skillButtons;

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

	public float globalCooldown = 1.5f;

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
		} else if (Input.GetKeyDown (KeyCode.R)) {
			OnSkill3ButtonClicked ();
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
		if (!skillButtons[0].interactable || !PlayerController.Instance.alive)
			return;
		
		PlayerController.Instance.BocDau ();
		foreach (Button skillButton in skillButtons) {
			if (skillButton == skillButtons [0]) {
				skillButton.interactable = false;
				StartCoroutine (CooldownSkill (skillButtons [0], PlayerController.Instance.skill1CooldownTime));
			} else if(skillButton.interactable) {
				skillButton.interactable = false;
				StartCoroutine (CooldownSkill (skillButton, globalCooldown));
			}
		}
	}

	public void OnSkill2ButtonClicked(){
		if (!skillButtons[1].interactable || !PlayerController.Instance.alive)
			return;
		
        StartCoroutine(PlayerController.Instance.INetBo());
		foreach (Button skillButton in skillButtons) {
			if (skillButton == skillButtons [1]) {
				skillButton.interactable = false;
				StartCoroutine (CooldownSkill (skillButtons [1], PlayerController.Instance.skill2CooldownTime));
			} else if(skillButton.interactable) {
				skillButton.interactable = false;
				StartCoroutine (CooldownSkill (skillButton, globalCooldown));
			}
		}
	}

	public void ActivateSkill3(){
		StopAllCoroutines ();
		skillButtons [0].interactable = false;
		skillButtons [1].interactable = false;

		StartCoroutine (AnimateSkill3Button ());
	}

	IEnumerator AnimateSkill3Button(){
		yield return new WaitForSeconds (1);

		skillButtons [2].interactable = true;
		skillButtons [2].gameObject.SetActive (true);

		float time = 0;
		while (true) {
			skillButtons [2].image.color = Color.Lerp (Color.white, Color.red, (Mathf.Sin(Mathf.PI * 4 * time / 0.5f) + 1)/2.0f);

			time += Time.deltaTime;
			yield return null;
		}
	}

	public void OnSkill3ButtonClicked(){
		if (!skillButtons[2].interactable || !PlayerController.Instance.alive)
			return;
		
		StopAllCoroutines ();

		PlayerController.Instance.GoiDoi ();
		skillButtons [2].image.color = Color.white;
		skillButtons [0].interactable = true;
		skillButtons [1].interactable = true;
		skillButtons [2].interactable = false;
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

﻿using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class TKSceneManager : MonoBehaviour
{
	public static readonly float SCREEN_FADE_TIME = 0.2f;

    public static readonly string SPLASH_SCENE = "SplashScene";
    public static readonly string MENU_SCENE = "MenuScene";
	public static readonly string GAME_PLAY_SCENE = "GamePlayScene";
    public static readonly string VICTORY_SCENE = "VictoryScene";
    public static readonly string GAME_OVER_SCENE = "GameOverScene";
	public static readonly string HUD_GAME_PLAY_SCENE = "HUDGamePlayScene";

    public static readonly string OPENING_1 = "Opening_S1";
    public static readonly string OPENING_2 = "Opening_S2";
    public static readonly string OPENING_3 = "Opening_S3";
    public static readonly string OPENING_4 = "Opening_S4";
    public static readonly string OPENING_5 = "Opening_S5";

    public static readonly string DEAD_1 = "Dead1";
    public static readonly string DEAD_2 = "Dead2";
    public static readonly string DEAD_3 = "Dead3";

    private static readonly List<string> popupScenes = new List<string> (){};
	private static readonly List<string> additiveScene = new List<string> ();

	private Image maskImage;
	private Color maskColor;

	private static GameObject createNewMask ()
	{
		GameObject mask = new GameObject ("Screen Masker");

		Camera mainCamera = Camera.main;

		RectTransform rTransform = mask.AddComponent<RectTransform> ();
		rTransform.position = new Vector3 (mainCamera.transform.position.x, mainCamera.transform.position.y, 0);
		rTransform.sizeDelta = new Vector2 ((mainCamera.orthographicSize * mainCamera.aspect + Mathf.Abs (mainCamera.transform.position.x)) * 2, (mainCamera.orthographicSize + Mathf.Abs (mainCamera.transform.position.y)) * 2);

		Canvas canvas = mask.AddComponent<Canvas> ();
		canvas.renderMode = RenderMode.WorldSpace;
		canvas.sortingOrder = 99;
		canvas.sortingLayerName = "Masks";

		Image img = mask.AddComponent<Image> ();
		Color color = Color.black;
		color.a = 0;
		img.color = color;

		return mask;
	}

	public static string GetCurrentSceneName(){
		return SceneManager.GetActiveScene ().name;
	}

	public static bool IsCurrentSceneEqual(string sceneName){
		return SceneManager.GetActiveScene ().name == sceneName;
	}

	public static void ChangeScene (string scene)
	{
		if (additiveScene.Contains (scene)) {
			SceneManager.LoadSceneAsync (scene, LoadSceneMode.Additive);
		} else {
			GameObject mask = createNewMask ();
			TKSceneManager controller = mask.AddComponent<TKSceneManager> ();
			controller.StartChangingScene (scene);
		}
	}

	public static void BackFromScene (GameObject popup)
	{
		GameObject mask = createNewMask ();
		TKSceneManager controller = mask.AddComponent<TKSceneManager> ();
		controller.StartDestroyingScene (popup);
	}

	private void StartDestroyingScene (GameObject scene)
	{
		StartCoroutine (MaskDestroyPopupScene (scene));
	}

	private void StartChangingScene (string scene)
	{
		if (popupScenes.Contains (scene)) {
			StartCoroutine (MaskLoadPopupScene (scene));
		} else {
			StartCoroutine (MaskLoadNewScene (scene));
		}
	}

	private IEnumerator MaskScreen ()
	{
		DontDestroyOnLoad (gameObject);

		maskImage = GetComponent<Image> ();
		maskColor = maskImage.color;

		float time = 0.0f;
		while (time < SCREEN_FADE_TIME) {
			time += Time.deltaTime;
			maskColor.a = Mathf.Lerp (0.0f, 1.0f, time / SCREEN_FADE_TIME);
			maskImage.color = maskColor;
			yield return null;
		}

		maskColor.a = 1;
		maskImage.color = maskColor;
		yield return new WaitForEndOfFrame ();
	}

	private IEnumerator UnmaskScreen ()
	{
		float time = 0.0f;
		while (time < SCREEN_FADE_TIME) {
			time += Time.deltaTime;
			maskColor.a = Mathf.Lerp (1.0f, 0.0f, time / SCREEN_FADE_TIME);
			maskImage.color = maskColor;
			yield return null;
		}

		Destroy (gameObject);
	}

	private IEnumerator MaskDestroyPopupScene (GameObject scene)
	{
		yield return StartCoroutine (MaskScreen ());

		Destroy (scene);

		yield return StartCoroutine (UnmaskScreen ());
	}

	private IEnumerator MaskLoadPopupScene (string scene)
	{
		yield return StartCoroutine (MaskScreen ());

		SceneManager.LoadScene (scene, LoadSceneMode.Additive);

		yield return StartCoroutine (UnmaskScreen ());
	}

	private IEnumerator MaskLoadNewScene (string scene)
	{
		yield return StartCoroutine (MaskScreen ());

		SceneManager.LoadScene (scene);

		yield return StartCoroutine (UnmaskScreen ());
	}
}
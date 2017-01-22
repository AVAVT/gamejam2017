using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine.UI;

public class Opening_S5 : MonoBehaviour {


    public AnimationClip clip;

	public Text textEnding;

	public float time = 0;
	public int i = 0;

	private List<string> textarr = new List<string>();
	public StreamReader sr = new StreamReader("Assets/Text/opening5.txt");

	void Start()
    {
		StartCoroutine (ChangeScene());
		AudioManager.Instance.PlayWaveSound ();
		string line;
		while ((line = sr.ReadLine()) != null)
		{
			textarr.Add(line);
		}
		setText ();
    }

	void Update()
	{
		time+= Time.deltaTime;

		if (time > i*2.5f)
		{
			setText();
		}
	}

	void setText()
	{
		if (i < textarr.Count)
		{
			textEnding.text = textarr[i++];
		}
		else
			textEnding.text = "";
	}

	void Awake(){
		StartCoroutine (ChangeScene());
	}

	IEnumerator ChangeScene()
	{
		yield return new WaitForSeconds (8f);
		TKSceneManager.ChangeScene(TKSceneManager.GAME_PLAY_SCENE);
	}
}

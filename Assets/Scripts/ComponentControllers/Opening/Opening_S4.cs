using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine.UI;

public class Opening_S4 : MonoBehaviour {
    public Transform meGiong;

	public Text textEnding;

	public float time = 0;
	public int i = 0;

	private List<string> textarr = new List<string>();
	public StreamReader sr = new StreamReader("Assets/Text/opening4.txt");

	void Start()
	{
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

		if (time > i*2.5)
		{
			setText();
		}

		meGiong.transform.Translate(new Vector3(200*Time.deltaTime, 0,0));
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
		yield return new WaitForSeconds (8);
		TKSceneManager.ChangeScene(TKSceneManager.OPENING_5);
	}
}

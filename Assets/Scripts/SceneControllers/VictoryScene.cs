using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class VictoryScene : MonoBehaviour {
    public Text textEnding;
    public Image imageCharacter;

	private bool animating = true;

	public float time = 0;
	public int i = 0;

	private List<string> textarr = new List<string>();
	public StreamReader sr = new StreamReader("Assets/Text/ending.txt");


	void Start(){
		string line;
		while ((line = sr.ReadLine()) != null)
		{
			textarr.Add(line);
		}
		setText ();
	}

    void Update()
    {
		if (!animating)
			return;
		
        if (imageCharacter.transform.position.y < 10)
        {
            imageCharacter.transform.Translate(new Vector3(-0.3f, 2.5f, 0));
        }

        else if (imageCharacter.transform.position.y <= 250)
        {
            imageCharacter.transform.Translate(new Vector3(1.5f, 2.5f, 0));
        }
        else
        {
            imageCharacter.transform.Translate(new Vector3(0.2f, 2.5f, 0));
        }

		if (imageCharacter.transform.localScale.x > 0) {
			imageCharacter.transform.localScale += new Vector3(-0.0015F, -0.0015F, 0);
		}
		else
        {
			animating = false;
			StartCoroutine (TheEnd ());
        }

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
	}

	IEnumerator TheEnd(){
		yield return new WaitForSeconds (1);

		textEnding.color = Color.clear;
		textEnding.text = "Thế là Chiến Sóng về trời...";
		float time = 0;
		while (time < 0.5f) {
			textEnding.color = Color.Lerp (Color.clear, Color.black, Mathfx.Sinerp (0, 1, time / 0.5f));

			time += Time.deltaTime;
			yield return null;
		}

		textEnding.color = Color.black;

		yield return new WaitForSeconds (2f);

		GoToMenu ();
	}

	void GoToMenu(){
		TKSceneManager.ChangeScene(TKSceneManager.MENU_SCENE);
	}
    
}

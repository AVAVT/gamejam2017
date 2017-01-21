using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class VictoryScene : MonoBehaviour {
    public Text textEnding;
    public Image imageCharacter;

    public int timeCout = 0;
    public int i = 0;

    Vector3 endPosition = new Vector3(108, 444, 0);
    private List<string> textarr = new List<string>();
    public StreamReader sr = new StreamReader("Assets/Text/ending.txt");

	void Start()
    {
		//Invoke ("GoToMenu", 1);
        string line;
        while ((line = sr.ReadLine()) != null)
        {
            textarr.Add(line);
        }
    }

    void Update()
    {
        timeCout++;
        int time = 0;

        if (timeCout % 2 == 0)
        {
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
            
                imageCharacter.transform.localScale += new Vector3(-0.0015F, -0.0015F, 0);
            if (imageCharacter.transform.localScale.x <= 0)
            {
                imageCharacter.transform.localScale = new Vector3(0,0, 0);
            }
        }

        if (timeCout > 150)
        {
            setText();
            timeCout = 0;
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

    void move()
    {
        
    }

	void GoToMenu(){
		TKSceneManager.ChangeScene(TKSceneManager.MENU_SCENE);
	}
    
}

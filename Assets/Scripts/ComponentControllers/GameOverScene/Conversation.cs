using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using System.IO;

public class Conversation : MonoBehaviour {
    public Text textConversation;
    public int i = 0;
    public int timeCount = 0;
    public StreamReader sr = new StreamReader("Assets/Text/ending.txt");

    private List<string> textarr = new List<string>();

	void Start () {
        //conversation("bac", "xyz");
        string line;

        while ((line = sr.ReadLine()) != null)
        {
            textarr.Add(line);
            print(line);
        }
	}
	
	// Update is called once per frame
	void Update () {
        timeCount++;
        if (timeCount > 100)
        {
            test();
            timeCount = 0;
        }
            
	}

    public void test()
    {
        if (i < textarr.Count)
            textConversation.text = textarr[i++];
    }
}

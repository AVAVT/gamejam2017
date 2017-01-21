using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SubText : MonoBehaviour {
    public float startTime = 0;
    public float endTime = 0;

    private Text subText;

    void Start()
    {
        subText = GetComponent<Text>();
        subText.color = new Color(255, 255, 255, 0);
    }
    void Update()
    {
        if ((Time.time > startTime && Time.time < endTime))
        {
            subText.color = new Color(255, 255, 255, 1);
        }
        else if (Time.time > endTime)
        {
            gameObject.SetActive(false);
        }
    }
}

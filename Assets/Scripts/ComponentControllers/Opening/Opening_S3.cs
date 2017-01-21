using UnityEngine;
using System.Collections;

public class Opening_S3 : MonoBehaviour { 

    private float rotateSpeed = 40;
    private float scaleSpeed = 15;

    public GameObject player;
    public GameObject bg;
	private bool loaded = false;
    void Update()
    {
		if (loaded)
			return;
		
        bg.transform.Rotate(0, 0, rotateSpeed*Time.deltaTime);

        if (player.transform.localScale.x < 100)
        {
            player.transform.localScale += new Vector3(1, 1, 1) * scaleSpeed * Time.deltaTime;
        }
        else
        {
			loaded = true;
            TKSceneManager.ChangeScene(TKSceneManager.OPENING_4);
        }
    }

    private void playMusic()
    {
        AudioSource aSource = GetComponent<AudioSource>();
        aSource.Play();
    }
}

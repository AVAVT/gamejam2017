using UnityEngine;
using System.Collections;

public class VideoPlayer : MonoBehaviour {

    private AudioSource aSource;

    public MovieTexture movie;

    Renderer render;
    void Start()
    {
        render = GetComponent<Renderer>();
        aSource = GetComponent<AudioSource>();
        render.material.mainTexture = movie as MovieTexture;

        PlayVideo();
    }

    void Update()
    {
        if(Time.time > movie.audioClip.length + 0.1f){
            // load scene
        }
    }
    private void PlayVideo()
    {
        aSource.Play();
        movie.Play();
    }
}

using UnityEngine;
using System.Collections;

public class VideoPlayer : MonoBehaviour {

    private AudioSource aSource;

    public MovieTexture movie;

    Renderer render;
    void OnEnable()
    {
        render = GetComponent<Renderer>();
        aSource = GetComponent<AudioSource>();
        render.material.mainTexture = movie as MovieTexture;

        PlayVideo();
    }
    private void PlayVideo()
    {
        aSource.Play();
        movie.Play();
    }
}

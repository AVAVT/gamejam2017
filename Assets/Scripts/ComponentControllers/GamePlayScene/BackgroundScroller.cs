using UnityEngine;
using System.Collections;

public class BackgroundScroller : MonoBehaviour {
	public static BackgroundScroller Instance { get; private set;}

	void Awake(){
		Instance = this;
	}

    public float scrollSpeed;
    public float tileSizeY;

    private Vector3 startPosition;

    void Start()
    {
        startPosition = transform.position;
    }

    void Update()
    {
        float newPosition = Mathf.Repeat(Time.time * scrollSpeed,tileSizeY);
        transform.position = startPosition + Vector3.up * newPosition;
    }
}

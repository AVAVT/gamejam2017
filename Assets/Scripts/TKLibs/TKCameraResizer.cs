using UnityEngine;
using System.Collections;

public class TKCameraResizer : MonoBehaviour {
	public static readonly float REFERENCE_DEVICE_HALF_WIDTH = 960.0f;

	void Awake(){
		Camera.main.orthographicSize = REFERENCE_DEVICE_HALF_WIDTH / Camera.main.aspect;
	}
}
using UnityEngine;
using System.Collections;

public class OpeningWaveWheelController : MonoBehaviour {
	
	void Update () {
		transform.localPosition = new Vector3 (0, Random.Range (-1.5f, 1.5f), 0);
	}
}

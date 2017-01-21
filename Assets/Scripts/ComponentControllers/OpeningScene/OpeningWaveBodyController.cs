using UnityEngine;
using System.Collections;

public class OpeningWaveBodyController : MonoBehaviour {

	// Update is called once per frame
	void Update () {
		transform.localPosition = new Vector3 (Random.Range (-1.5f, 1.5f), Random.Range (-1.5f, 1.5f), 0);
	}
}

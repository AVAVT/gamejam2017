﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AllyExhaustController : MonoBehaviour {

	public List<GameObject> prefabs;
	public GameObject netBoFire;

	private List<GameObject> smokes = new List<GameObject>();
	// Use this for initialization
	void Start () {
		StartCoroutine (EmitSmoke ());
	}

	GameObject GetSmoke(){
		foreach (GameObject smoke in smokes) {
			if (!smoke.activeInHierarchy) {
				return smoke;
			}
		}
		return AddSmoke ();
	}

	GameObject AddSmoke(){
		GameObject smoke = TKUtils.Instantiate (prefabs [Random.Range (0, prefabs.Count)]);
		smokes.Add (smoke);
		return smoke;
	}

	IEnumerator EmitSmoke(){
		while (true) {
			yield return new WaitForSeconds (Random.Range (0.03f, 0.11f));

			if (!netBoFire.activeInHierarchy) {
				GameObject smoke = GetSmoke ();
				smoke.transform.position = transform.position;
				smoke.gameObject.SetActive (true);
			}
		}
	}

	public void NetBoFire(){
		netBoFire.SetActive (true);
	}
}

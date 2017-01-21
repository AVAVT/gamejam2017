using UnityEngine;
using System.Collections;

public class NetBoFireController : MonoBehaviour {

	private Animator anim;

	void Awake(){
		anim = GetComponent<Animator> ();	
	}

	void OnEnable(){
		anim.Play ("NetBoFire");
		StartCoroutine (Finish ());
	}

	IEnumerator Finish(){
		yield return new WaitForSeconds (3);
		gameObject.SetActive (false);
	}
}

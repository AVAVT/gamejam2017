using UnityEngine;
using System.Collections;

public class NetBoFireController : MonoBehaviour {

	private Animator anim;

	void Awake(){
		anim = GetComponent<Animator> ();	
	}

	void OnEnable(){
		anim.Play ("NetBoFire");
	}

	public void Finished(){
		gameObject.SetActive (false);
	}
}

using UnityEngine;
using System.Collections;

public class RotatingSpikesPlatform : MonoBehaviour {

	//Script for rotating spiked platform hazard/mechanic.

	Animator anim;
	public bool rotateRight;
	// Use this for initialization
	void Start () {
		anim = gameObject.GetComponent<Animator> ();
		rotateRight = true;
	}
	
	// Update is called once per frame
	void Update () {
		if (rotateRight == false) {
			anim.SetBool("RotateRight", false);
		}
		if (rotateRight == true) {
			anim.SetBool("RotateRight", true);
		}
	}



}

using UnityEngine;
using System.Collections;

public class Door : MonoBehaviour {

	//Script controls animators animation states for door object. Door.cs is used with Switch.cs.
	// Check "Switches" object in the scene for further references.

	Animator anim;
	public bool OpenDoor;

	// Use this for initialization
	void Start () {
		anim = gameObject.GetComponent<Animator> ();
		OpenDoor = false;
		anim.SetBool ("OpenDoor", false);
	}

	void DoorManipulate()
	{
		if (OpenDoor == false) {
			OpenDoor = true;
		} else {
			OpenDoor = false;
		}

		if (OpenDoor == false) {
			anim.SetBool("OpenDoor", false);
		}
		if (OpenDoor == true) {
			anim.SetBool("OpenDoor", true);
		}
	}

}

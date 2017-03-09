using UnityEngine;
using System.Collections;

public class MetaBoxTrigg : MonoBehaviour {

	public bool triggerMenaged;

	// Use this for initialization
	void Start () {
		triggerMenaged = false;
	
	}
	
	// Update is called once per frame
	void Update () {
		/*
		if (triggerMenaged == false) {
			gameObject.GetComponentInChildren<BoxCollider2D> ().isTrigger = true;
			triggerMenaged = true;
		}
		*/
	}
	void FixedUpdate()
	{
		
	}
}

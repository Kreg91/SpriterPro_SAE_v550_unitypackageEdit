using UnityEngine;
using SpriterDotNetUnity;
using System;
using System.Linq;
using System.Collections.Generic;
using SpriterDotNet;
using System.Collections;

public class Switch : MonoBehaviour {

	//Script for controling Switch mechanic animation and sending message to a other object that will be contected to it. It referes to SpriterPro .scml prefab.
	// Check "Switches" object in the scene for further references.

	public float disableMoveTime;

	private bool switchReadyToUseState;
	private bool switchWasUsedState;
	private bool switchUseingState;

	public float switchRestartTime;

	private GameObject player;

	private UnitySpriterAnimator animator;

	public GameObject ObjectThatSwitchIsUsedOn; // insert a object from the scene
	public string methodNameOfSwitchObject;

	// Use this for initialization
	void Start () {
		
		disableMoveTime = 0.5f;
		switchReadyToUseState = true;
		switchRestartTime = 1f;
		player = GameObject.FindWithTag ("Player");
	}

	// Update is called once per frame
	void Update () {
		if (animator == null)
		{
			animator = gameObject.GetComponent<SpriterDotNetBehaviour>().Animator;
			animator.EventTriggered += e => Debug.Log("Event Triggered. Source: " + animator.CurrentAnimation.Name + ". Value: " + e);
		}
		SwitchReadyToUseAnim ();
		SwitchWasUsedAnim ();
	//	AnimationState ();

	}

	IEnumerator OnTriggerEnter2D(Collider2D col)
	{
		//Switch is used after coliding with a axe swing of player character
		if (switchReadyToUseState == true) {
			if (col.gameObject.tag == "Axe" ) { //this name is generated in the spriter collision box in hero_axe attack animation
				switchReadyToUseState = false;
				switchUseingState = true;
				col.gameObject.GetComponentInParent<Hero2DUserControl> ().moveEnabled = false;
	
				SwitchActivationAnim ();
				SendMessageToAssocjatedObject (); // Send a message to another object after useing the switch

				//Disable players move for a time
				yield return new WaitForSeconds (disableMoveTime);
				col.gameObject.GetComponentInParent<Hero2DUserControl> ().moveEnabled = true;

				//Reset the Switch
				StartCoroutine (Reset ());
			}
		}
	}

	void SendMessageToAssocjatedObject()
	{
		ObjectThatSwitchIsUsedOn.SendMessage(methodNameOfSwitchObject, SendMessageOptions.DontRequireReceiver);
	}

	void SwitchReadyToUseAnim()
	{
		if (switchReadyToUseState == true) {
			if (animator.Progress >= 0.85f) {
				animator.Play ("Switch_Off");

			} else if (animator.Progress < 0.95f) {
				//Debug.Log (animator.Time + "time");
				//Debug.Log (animator.Progress + "progres");
			}
		}
	}

	void SwitchActivationAnim()
	{
		if (switchUseingState == true) {
			animator.Play ("Switch_ActionOffToOn");
			switchUseingState = false;
			switchReadyToUseState = false;
			switchWasUsedState = true;
		}
	}


	void SwitchWasUsedAnim(){
		if (switchWasUsedState == true) {
			if (animator.Progress >= 0.85f) {
				animator.Play ("Switch_On");
			} else if (animator.Progress < 0.95f) {
				//Debug.Log (animator.Time + "time");
				//Debug.Log (animator.Progress + "progres");
			}
		}
	}

	IEnumerator Reset()
	{
		yield return new WaitForSeconds (switchRestartTime);
		switchReadyToUseState = true;

	}

	void AnimationState()
	{
		Debug.Log (animator.CurrentAnimation.Name);
	}

}

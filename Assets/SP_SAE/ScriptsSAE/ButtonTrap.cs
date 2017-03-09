using UnityEngine;
using SpriterDotNetUnity;
using System;
using System.Linq;
using System.Collections.Generic;
using SpriterDotNet;
using UnityEngine.UI;
using System.Text;
using System.Collections;

public class ButtonTrap : MonoBehaviour {

	//Script for button trap hazard animations. It refers to SpriterPro .scml prefab.

	public float disableMoveTime;

	private bool trapReadyToUseState;
	private bool trapWasUsedState;
	private bool trapUseingState;

	public float trapRestartTime;

	private GameObject player;

	private UnitySpriterAnimator animator;

	public GameObject missleSpawnPointObject; // insert a spawn point from the scene to the inspector
	//public string SpawnMethodName;
	public GameObject missleToSpawn; // Insert a missle prefab to this field in inspector.
	public Vector3 v3_missleSpawnVelocity;


	// Use this for initialization
	void Start () {
		trapReadyToUseState = true;
		trapRestartTime = 1.0f;
		player = GameObject.FindWithTag ("Player");

		missleSpawnPointObject = gameObject.GetComponentInChildren<missleSpawner> ().gameObject;
	}

	// Update is called once per frame
	void Update () {
		if (animator == null)
		{
			animator = gameObject.GetComponent<SpriterDotNetBehaviour>().Animator;
			animator.EventTriggered += e => Debug.Log("Event Triggered. Source: " + animator.CurrentAnimation.Name + ". Value: " + e);
		}
		TrapReadyToUseAnim ();
		TrapWasUsedAnim ();
	//	AnimationState ();
	}

	IEnumerator OnTriggerEnter2D(Collider2D col)
	{
		if (trapReadyToUseState == true) {
			if (col.gameObject.tag == "Player") {
				trapReadyToUseState = false;
				trapUseingState = true;

				TrapActivationAnim ();
				SpawnMissle ();

				//Reset trap
				yield return new WaitForSeconds (trapRestartTime);
				trapReadyToUseState = true;
			}
		}
	}

	void SpawnMissle()
	{
		gameObject.GetComponentInChildren<missleSpawner> ().SpawnMissleFunction ();
		//missleSpawnPointObject.SendMessage(SpawnMethodName, SendMessageOptions.DontRequireReceiver);
	}

	void TrapReadyToUseAnim()
	{

		if (trapReadyToUseState == true) {
			if (animator.Progress >= 0.85f) {
				animator.Play ("Button_Off");
			} else if (animator.Progress < 0.95f) {
				//Debug.Log (animator.Time + "time");
				//Debug.Log (animator.Progress + "progres");
			}
		}
	}

	void TrapActivationAnim()
	{
		if (trapUseingState == true) {
			animator.Play ("Button_Hit");
			trapUseingState = false;
			trapReadyToUseState = false;
			trapWasUsedState = true;
		}
	}


	void TrapWasUsedAnim(){
		if (trapWasUsedState == true) {
			if (animator.Progress >= 0.85f) {
				animator.Play ("Button_On");
			} else if (animator.Progress < 0.95f) {
				//Debug.Log (animator.Time + "time");
				//Debug.Log (animator.Progress + "progres");
			}
			Reset ();
		}
	}

	IEnumerator Reset()
	{
		yield return new WaitForSeconds (trapRestartTime);
		trapReadyToUseState = true;

	}

	void AnimationState()
	{
		Debug.Log (animator.CurrentAnimation.Name);
	}

}

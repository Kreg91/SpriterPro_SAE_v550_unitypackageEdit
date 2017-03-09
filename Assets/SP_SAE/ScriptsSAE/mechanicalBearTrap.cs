using UnityEngine;
using SpriterDotNetUnity;
using System;
using System.Linq;
using System.Collections.Generic;
using SpriterDotNet;
using UnityEngine.UI;
using System.Text;
using System.Collections;

public class mechanicalBearTrap : MonoBehaviour {

	//Script for mechanical bear trap hazard animations. Script includes influnce on player character. It refers to SpriterPro .scml prefab.

	private float xCord_mechTrap;
	private float yCord_mechTrap;

	private float xCord_player;
	private float yCord_player;

	private Rigidbody2D player_rig;

	public float velocityHit;

	public float disableMoveTime;

	private bool trapReadyToUseState;
	private bool trapWasUsedState;
	private bool trapUseingState;

	public float trapRestartTime;

	public GameObject player;

	private UnitySpriterAnimator animator;

	// Use this for initialization
	void Start () {
		xCord_mechTrap = gameObject.transform.position.x;
		yCord_mechTrap = gameObject.transform.position.y;
		velocityHit = 5.0f;
		disableMoveTime = 0.5f;
		trapReadyToUseState = true;
		trapRestartTime = 0.5f;
		player = GameObject.FindWithTag ("Player");
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
		//AnimationState ();
	
	}

	IEnumerator OnTriggerEnter2D(Collider2D col)
	{
		if (trapReadyToUseState == true) {
			if (col.gameObject.tag == "Player") {
				trapReadyToUseState = false;
				trapUseingState = true;
				player_rig = col.gameObject.GetComponent<Rigidbody2D> ();
				xCord_player = col.gameObject.transform.position.x;
				yCord_player = col.gameObject.transform.position.y;

				col.gameObject.GetComponent<Hero2DUserControl> ().moveEnabled = false;

				if ((xCord_player - xCord_mechTrap) > 0.0f) {
				//	player_rig.velocity = new Vector2 ( velocityHit, velocityHit);
				} else if ((xCord_player - xCord_mechTrap) <= 0.0f)
				{
				//	player_rig.velocity = new Vector2 ( -velocityHit , velocityHit);
				}

				//Enable player anim, and enable trap anim
				DemagePlayer ();
				TrapActivationAnim ();
				// enable animations END.

				yield return new WaitForSeconds (disableMoveTime);
				col.gameObject.GetComponent<Hero2DUserControl> ().moveEnabled = true;
				//Disable this comment bellow, if you whant to reset the bearTrap, after using it.
				//StartCoroutine(Reset());

			}
		}
	}

	void DemagePlayer()
	{
		player.GetComponent<Hero2DAnimControl> ().anim_getDemageBlood = true;
		player.GetComponent<Hero2DStats> ().DemageHero (50.0f);
	}

	void TrapReadyToUseAnim()
	{
		if (trapReadyToUseState == true) {
			if (animator.Progress >= 0.99f) {
				animator.Play ("Trap_ReadyToUse");

			} else if (animator.Progress < 0.95f) {
				//Debug.Log (animator.Time + "time");
				//Debug.Log (animator.Progress + "progres");
			}
		}
	}

	void TrapActivationAnim()
	{
		if (trapUseingState == true) {
			animator.Play ("Trap_Useing");
			trapUseingState = false;
			trapReadyToUseState = false;
			trapWasUsedState = true;
		}
	}

	void TrapWasUsedAnim(){
		if (trapWasUsedState == true) {
			if (animator.Progress >= 0.85f) {
				animator.Play ("Trap_Used");

			} else if (animator.Progress < 0.95f) {
				//Debug.Log (animator.Time + "time");
				//Debug.Log (animator.Progress + "progres");
			}
		
		}
	}

	IEnumerator Reset()
	{
		yield return new WaitForSeconds (trapRestartTime);
		trapReadyToUseState = true;
		trapWasUsedState = false;
		trapUseingState = false;
	}

	void AnimationState()
	{
			Debug.Log (animator.CurrentAnimation.Name);
	}

}

using UnityEngine;
using SpriterDotNetUnity;
using System;
using System.Linq;
using System.Collections.Generic;
using SpriterDotNet;
using UnityEngine.UI;
using System.Text;
using System.Collections;

public class FireTrap : MonoBehaviour {

	//Script for fire trap hazard animations. It also controls the flame damage and influence on player character. It refers to SpriterPro .scml prefab.

	private float xCord_FireTrap;
	private float yCord_FireTrap;

	private float xCord_player;
	private float yCord_player;

	private Rigidbody2D player_rig;

	public float velocityHit;

	public float disableMoveTime;

	// Set fire trap prefab in the scene and check fireTrapMode1-3 as you like
	public bool fireTrapMod1;
	public bool fireTrapMod2;
	public bool fireTrapMod3;
	// Set this in inspector or in Start method
	public bool fireDemageable;

	public float trapRestartTime;

	private GameObject player;

	private UnitySpriterAnimator animator;

	// Use this for initialization
	void Start () {
		xCord_FireTrap = gameObject.transform.position.x;
		yCord_FireTrap = gameObject.transform.position.y;
		velocityHit = 2.0f;
		disableMoveTime = 0.5f;
		trapRestartTime = 0.5f;
		player = GameObject.FindWithTag ("Player");

		fireDemageable = true;
	}

	// Update is called once per frame
	void Update () {
		SetSpriterAnimator ();
		FireTrapMod1 ();
		FireTrapMod2 ();
		FireTrapMod3 ();
	}

	IEnumerator OnTriggerStay2D(Collider2D col)
	{
		if (fireDemageable == true) {
			if (col.gameObject.tag == "Player") {
				fireDemageable = false;
				player_rig = col.gameObject.GetComponent<Rigidbody2D> ();
				xCord_player = col.gameObject.transform.position.x;
				yCord_player = col.gameObject.transform.position.y;

				col.gameObject.GetComponent<Hero2DUserControl> ().moveEnabled = false;

				if ((xCord_player - xCord_FireTrap) > 0.0f) {
					//	player_rig.velocity = new Vector2 ( velocityHit, velocityHit);
				} else if ((xCord_player - xCord_FireTrap) <= 0.0f)
				{
					//	player_rig.velocity = new Vector2 ( -velocityHit , velocityHit);
				}

				DemagePlayer (25.0f);

				yield return new WaitForSeconds (disableMoveTime);
				col.gameObject.GetComponent<Hero2DUserControl> ().moveEnabled = true;

				StartCoroutine (Reset ());

			}
		}
	}

	void DemagePlayer(float dmg)
	{
		player.GetComponent<Hero2DAnimControl> ().anim_getDemageBlood = true;
		player.GetComponent<Hero2DStats> ().DemageHero (dmg);
	}

	void FireTrapMod1()
	{

		if (fireTrapMod1 == true) {
			/*
			if (animator.CurrentAnimation.Name != "Trap_ReadyToUse") {
				animator.Transition ("Trap_ReadyToUse", anim_Transition_Time);
			}
			*/
			if (animator.Progress >= 0.95f) {
				animator.Play ("state_flaming_CASCADING");

			} else if (animator.Progress < 0.95f) {
				//Debug.Log (animator.Time + "time");
				//Debug.Log (animator.Progress + "progres");
			}
		}
	}

	void FireTrapMod2()
	{
		if (fireTrapMod2 == true) {
			if (animator.Progress >= 0.85f) {
				animator.Play ("state_flaming_CASCADING_000");

			} else if (animator.Progress < 0.95f) {
				//Debug.Log (animator.Time + "time");
				//Debug.Log (animator.Progress + "progres");
			}
		}
	}


	void FireTrapMod3(){
		if (fireTrapMod3 == true) {
			/*
			if (animator.CurrentAnimation.Name != "Trap_Used") {
				animator.Transition ("Trap_Used", anim_Transition_Time);
			}

			*/
			if (animator.Progress >= 0.85f) {
				animator.Play ("state_flaming_CASCADING_001");

			} else if (animator.Progress < 0.95f) {
				//Debug.Log (animator.Time + "time");
				//Debug.Log (animator.Progress + "progres");
			}
		}
	}

	IEnumerator Reset()
	{
		yield return new WaitForSeconds (trapRestartTime);
		fireDemageable = true;

	}

	void AnimationState()
	{
		Debug.Log (animator.CurrentAnimation.Name);
	}


	void SetSpriterAnimator()
	{
		if (animator == null)
		{
			animator = gameObject.GetComponentInParent<SpriterDotNetBehaviour>().Animator;
			animator.EventTriggered += e => Debug.Log("Event Triggered. Source: " + animator.CurrentAnimation.Name + ". Value: " + e);
		}
	}
}

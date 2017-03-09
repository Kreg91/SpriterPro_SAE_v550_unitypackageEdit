using UnityEngine;
using SpriterDotNetUnity;
using System;
using System.Linq;
using System.Collections.Generic;
using SpriterDotNet;
using UnityEngine.UI;
using System.Text;
using System.Collections;

public class Chain : MonoBehaviour {

	////Script for chain hazard animations. It also controls the chain hit influence on player character. It refers to SpriterPro .scml prefab.

	private float xCord_Chain;
	private float yCord_Chain;

	private float xCord_player;
	private float yCord_player;

	private Rigidbody2D player_rig;

	public float velocityHit;

	public float disableMoveTime;

	public bool chainOn;
	public bool chainBig;

	public bool chainRight;

	public bool chainDemageOn;

	public float chainRestartTime;

	private GameObject player;

	public UnitySpriterAnimator animator;
	private GameObject animatorChain;

	public float chainSpeed;

	// Use this for initialization
	void Start () {
		xCord_Chain = gameObject.transform.position.x;
		yCord_Chain = gameObject.transform.position.y;
		velocityHit = 5.0f;
		disableMoveTime = 0.5f;
		chainOn = true;
		chainRestartTime = 0.3f;
		player = GameObject.FindWithTag ("Player");

		chainBig = true;

		chainDemageOn = true;

		chainSpeed = 1.0f;
	}

	// Update is called once per frame
	void Update () {
		if (animator == null)
		{
			animator = gameObject.GetComponentInParent<SpriterDotNetBehaviour>().Animator;
			animator.EventTriggered += e => Debug.Log("Event Triggered. Source: " + animator.CurrentAnimation.Name + ". Value: " + e);
		}
		animator.Speed = chainSpeed;
		ChainOff ();
		ChainLeft ();
		ChainRight ();
	}

	IEnumerator OnTriggerEnter2D(Collider2D col)
	{
		if (chainOn == true && chainDemageOn == true) {
			if (col.gameObject.tag == "Player") {
				chainDemageOn = false;
				player_rig = col.gameObject.GetComponent<Rigidbody2D> ();
				xCord_player = col.gameObject.transform.position.x;
				yCord_player = col.gameObject.transform.position.y;

				col.gameObject.GetComponent<Hero2DUserControl> ().moveEnabled = false;

				//Add velocity to Player after hit
				if ((xCord_player - xCord_Chain) > 0.0f && (yCord_player - yCord_Chain) > 0.0f) {
						player_rig.velocity = new Vector2 ( velocityHit, velocityHit);
				}else if((xCord_player - xCord_Chain) > 0.0f && (yCord_player - yCord_Chain) <= 0.0f)
				{
					player_rig.velocity = new Vector2 ( velocityHit, -velocityHit);
				}
				else if ((xCord_player - xCord_Chain) <= 0.0f && (yCord_player - yCord_Chain) > 0.0f)
				{
						player_rig.velocity = new Vector2 ( -velocityHit , velocityHit);
				}else if((xCord_player - xCord_Chain) <= 0.0f && (yCord_player - yCord_Chain) <= 0.0f)
				{
					player_rig.velocity = new Vector2 ( -velocityHit , -velocityHit);
				}

				//Enable player anim
				DemagePlayer ();
				yield return new WaitForSeconds (disableMoveTime);
				col.gameObject.GetComponent<Hero2DUserControl> ().moveEnabled = true;
				yield return new WaitForSeconds (chainRestartTime);
				chainDemageOn = true;
			}
		}
	}

	void DemagePlayer()
	{
		player.GetComponent<Hero2DAnimControl> ().anim_getDemageBlood = true;
		player.GetComponent<Hero2DStats> ().DemageHero (50.0f);
	}

	void ChainOff()
	{
		if (chainOn == false) {

			if (animator.Progress >= 0.95f) {
				if (chainBig == false) {
					animator.Play ("chain_Off");
				} else if (chainBig == true) {
					animator.Play ("chain_Off_BIG");
				}
			} 
		}
	}
		
	void ChainLeft(){
		if (chainRight == false && chainOn == true) {
			
			if (animator.Progress >= 0.95f || (animator.CurrentAnimation.Name == "chain_Off" || animator.CurrentAnimation.Name == "chain_Off_BIG" )) {
				if (chainBig == false) {
					animator.Play ("chain_rotatingLeft_parts");
				} else if (chainBig == true) {
					animator.Play ("chain_rotatingLeft_parts_BIG");
				}
			} else if (animator.Progress < 0.99f) {
				//Debug.Log (animator.Time + "time");
				//Debug.Log (animator.Progress + "progres");
			}
		}
	}

	void ChainRight()
	{
		if (chainRight == true && chainOn == true) {

			if (animator.Progress >= 0.95f || (animator.CurrentAnimation.Name == "chain_Off" || animator.CurrentAnimation.Name == "chain_Off_BIG" )) {
				if (chainBig == false) {
					animator.Play ("chain_rotatingRight_parts");
				} else if (chainBig == true) {
					animator.Play ("chain_rotatingRight_parts_BIG");
				}
			} else if (animator.Progress < 0.95f) {
				//Debug.Log (animator.Time + "time");
				//Debug.Log (animator.Progress + "progres");
			}
		}
	}

	IEnumerator Reset()
	{
		yield return new WaitForSeconds (chainRestartTime);
		chainOn = true;

	}
		
	void ChainOnOff()
	{
		if (chainOn == true)
			{ 
				chainOn = false; 
			} else {
			chainOn = true;
		}
	}

	void ChainSize()
	{
		if (chainBig == true)
		{ 
			chainBig = false; 
		} else {
			chainBig = true;
		}
	}

	void ChainDirection()
	{
		if (chainRight == true)
		{ 
			chainRight = false; 
		} else {
			chainRight = true;
		}
	}

	void AnimationState()
	{
		Debug.Log (animator.CurrentAnimation.Name);
	}
}

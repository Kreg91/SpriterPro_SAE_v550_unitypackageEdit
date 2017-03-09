using UnityEngine;
using SpriterDotNetUnity;
using System;
using System.Linq;
using System.Collections.Generic;
using SpriterDotNet;
using UnityEngine.UI;
using System.Text;
using System.Collections;

public class BarrelExplo : MonoBehaviour {

	//Script for barrel hazard animations. It also controls the explosion influence on player character. It refers to SpriterPro .scml prefab.

	private float xCord_barrel;
	private float yCord_barrel;

	private float xCord_player;
	private float yCord_player;

	private Rigidbody2D player_rig;

	public float velocityHit;

	public float disableMoveTime;

	private bool trapReadyToUseState;
	private bool trapTicking;
	private bool trapExplosionState;

	public bool barrelDestroyState;

	public bool barrelDemagable;

	public float trapRestartTime;

	private GameObject player;
	private GameObject barrelParent;

	public UnitySpriterAnimator animator;

	// Use this for initialization
	void Start () {
		xCord_barrel = gameObject.transform.position.x;
		yCord_barrel = gameObject.transform.position.y;
		velocityHit = 5.0f;
		disableMoveTime = 0.5f;
		trapReadyToUseState = true;
		trapRestartTime = 0.5f;
		player = GameObject.FindWithTag ("Player");
		barrelParent = gameObject.GetComponentInParent<SpriterDotNetBehaviour>().gameObject;
		barrelDemagable = true;
	}

	// Update is called once per frame
	void Update () {
		SetSpriterAnimator ();
		TrapReadyToUseAnim ();
		TrapExplosion ();
		//AnimationState ();

	}

	IEnumerator OnTriggerEnter2D(Collider2D col)
	{
		if ((trapReadyToUseState == true) && (animator.CurrentAnimation.Name == "barrel_static" )) {
			if (col.gameObject.tag == "Player" || col.gameObject.tag == "Axe") {
				trapReadyToUseState = false;
				trapTicking = true;
				TrapTicking ();
			}
		} else if (animator.CurrentAnimation.Name == "barrel_explosion") 
		{
			if (col.gameObject.tag == "Player" ) {
				player_rig = col.gameObject.GetComponent<Rigidbody2D> ();
				xCord_player = col.gameObject.transform.position.x;
				yCord_player = col.gameObject.transform.position.y;
			
				//Enable player anim, and enable trap anim
				if (barrelDemagable == true){

					col.gameObject.GetComponent<Hero2DUserControl> ().moveEnabled = false;

					if ((xCord_player - xCord_barrel) > 0.0f) {
						player_rig.velocity = new Vector2 ( velocityHit, velocityHit);
					} else if ((xCord_player - xCord_barrel) <= 0.0f)
					{
						player_rig.velocity = new Vector2 ( -velocityHit , velocityHit);
					}
					DemagePlayer (40.0f);
					barrelDemagable = false;
				}

				Debug.Log ("explosion and collision with player");
				yield return new WaitForSeconds (disableMoveTime);
				col.gameObject.GetComponent<Hero2DUserControl> ().moveEnabled = true;
			}
		}
	}

	void DemagePlayer(float dmg)
	{
		player.GetComponent<Hero2DAnimControl> ().anim_getDemageBlood = true;
		player.GetComponent<Hero2DStats> ().DemageHero (dmg);
	}

	void TrapReadyToUseAnim()
	{

		if (trapReadyToUseState == true) {
			
			if (animator.Progress >= 0.99f) {
				animator.Play ("barrel_static");

			} else if (animator.Progress < 0.95f) {
				//Debug.Log (animator.Time + "time");
				//Debug.Log (animator.Progress + "progres");
			}
		}
	}

	void TrapTicking()
	{
		if (trapTicking == true) {
			animator.Play ("barrel_counting");
			trapTicking = false;
			trapReadyToUseState = false;
			trapExplosionState = true;
		}
	}
		
	void TrapExplosion(){
		if ((trapExplosionState == true) && (animator.CurrentAnimation.Name == "barrel_counting") ) {
			
			if (animator.Progress >= 0.85f) {
				animator.Play ("barrel_explosion");
				trapExplosionState = false;
				barrelDestroyState = true;
			} else if (animator.Progress < 0.95f) {
				
			}
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
		
	void SetSpriterAnimator()
	{
		if (animator == null)
		{
			animator = gameObject.GetComponentInParent<SpriterDotNetBehaviour>().Animator;
			animator.EventTriggered += e => Debug.Log("Event Triggered. Source: " + animator.CurrentAnimation.Name + ". Value: " + e);
		}
	}
}

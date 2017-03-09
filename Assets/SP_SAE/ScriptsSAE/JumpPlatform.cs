using UnityEngine;
using SpriterDotNetUnity;
using System;
using System.Linq;
using System.Collections.Generic;
using SpriterDotNet;
using UnityEngine.UI;
using System.Text;
using System.Collections;

public class JumpPlatform : MonoBehaviour {

	//Script for Jumping Platform animation states control. It refers to SpriterPro .scml prefab.

	private UnitySpriterAnimator animator;
	private bool Jump;
	public float anim_Progress;
	public float anim_Progress_constEnd;
	public float anim_Transition_Time;
	public float JumpVelocity;
	public float JumpResetTime;
	private bool jumpReady;

	void Start()
	{
		JumpResetTime = 0.2f;
		anim_Progress_constEnd = 0.95f;
		JumpVelocity = 20.0f;
		anim_Transition_Time = 1f;
		jumpReady = true;
	}
	
	// Update is called once per frame
	void Update () {
		if (animator == null)
		{
			animator = gameObject.GetComponent<SpriterDotNetBehaviour>().Animator;
			animator.EventTriggered += e => Debug.Log("Event Triggered. Source: " + animator.CurrentAnimation.Name + ". Value: " + e);
		}
		Anim_On ();
	
	}

	/*
	void OnTriggerEnter2D(Collider2D col)
	{	if (col.gameObject.tag == "Player") {
			if (jumpReady == true) {
				jumpReady = false;
				StartCoroutine ("Reset");
				Anim_Jump ();
			}
		}

	}
	*/

	void OnCollisionEnter2D(Collision2D col2d)
	{
		if (col2d.gameObject.tag == "Player") {
			if (jumpReady == true) {
				col2d.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0.0f, JumpVelocity);
				jumpReady = false;
				StartCoroutine ("Reset");
				Anim_Jump ();
			}
		}
	}

	IEnumerator Reset()
	{
		yield return new WaitForSeconds (JumpResetTime);
		jumpReady = true;
	}

	void Anim_Jump()
	{		// Sets the streching animation for the platform after Character jumps on it.
			if (animator.CurrentAnimation.Name != "JumpP_JUMP") {
				animator.Transition ("JumpP_JUMP", anim_Transition_Time);
			}
			if (animator.Progress >= 0.95f) {
				animator.Play ("JumpP_JUMP");

			} else if (animator.Progress < 0.95f) {
				//Debug.Log (animator.Time + "time");
				//Debug.Log (animator.Progress + "progres");
			}
	}
	void Anim_On()
	{
		//Sets the static form for the platform (ready to be jumped on)
		if (jumpReady == true) {
			if (animator.CurrentAnimation.Name != "JumpP_Static") {
				animator.Transition ("JumpP_Static", anim_Transition_Time);
			}

			if (animator.Progress >= 0.95f) {
				animator.Play ("JumpP_Static");

			} else if (animator.Progress < 0.95f) {
				//Debug.Log (animator.Time + "time");
				//Debug.Log (animator.Progress + "progres");
			}
		}
	}

}

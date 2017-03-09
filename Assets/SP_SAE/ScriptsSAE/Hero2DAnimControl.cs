
///////////////////////////////////////////////////////////////
//This note applyes to code bellow "LICENSE start" comment.	 //	
// Copyright (c) 2015 The original author or authors         //
//																//
// This software may be modified and distributed under the terms//
// of the zlib license.  See the LICENSE file for details.		//
//////////////////////////////////////////////////////////////////


//SpriterPro Scripting Animation Examples code START

//Script mainly for controling players character animation states from the SpriterPro .scml file.

using UnityEngine;
using SpriterDotNetUnity;
using System;
using System.Linq;
using System.Collections.Generic;
using SpriterDotNet;
using UnityEngine.UI;
using System.Text;
using System.Collections;


public class Hero2DAnimControl : MonoBehaviour {



	public float HeroSpeed;

	public Text Text;

	public float MaxSpeed = 5.0f;
	public float DeltaSpeed = 0.2f;
	public float TransitionTime = 0.5f;

	[HideInInspector]
	public float AnimatorSpeed = 1.0f;

	public UnitySpriterAnimator animator;

	public bool anim_Walk;
	public bool anim_Idle;
	public bool anim_axe_disableMove;
	public bool anim_getDemageBlood;
	public bool anim_HeroDead;
	public bool anim_HeroAgony;

	private float anim_Progress;
	private float anim_Progress_constEnd;
	private float anim_Transition_Time;

	void Start()
	{
		HeroSpeed = 0.0f;
		anim_Progress_constEnd = 0.99f;
		anim_Idle = false;
		anim_Walk = false;
		anim_getDemageBlood = false;
		anim_HeroDead = false;
		anim_Transition_Time = 1f;
	}



	void Update()
	{
		
		if (animator == null)
		{
			animator = gameObject.GetComponentInChildren<SpriterDotNetBehaviour>().Animator;
			animator.EventTriggered += e => Debug.Log("Event Triggered. Source: " + animator.CurrentAnimation.Name + ". Value: " + e);
		}
		Anim_WalkRunMode ();
		AnimatorController ();
	}

	void FixedUpdate()
	{
		if (anim_Progress >= anim_Progress_constEnd) {
			anim_Idle = false;
			anim_Walk = false;
		}
		HeroSpeed = Input.GetAxis("Horizontal");
	}

	void Anim_Walk()
	{
		if ((HeroSpeed > 0.0f || HeroSpeed < 0.0f) && anim_Walk == true && anim_axe_disableMove==false && anim_getDemageBlood != true && anim_HeroDead != true) {

			if (animator.CurrentAnimation.Name != "Hero_walk") {
				animator.Transition ("Hero_walk", anim_Transition_Time);
			}

			if (animator.Progress >= 0.95f) {
				animator.Play ("Hero_walk");

			} else if (animator.Progress < 0.95f) {
				//Debug.Log (animator.Time + "time");
				//Debug.Log (animator.Progress + "progres");
			}
		}
	}

	void Anim_Run()
	{
		if ((HeroSpeed > 0.0f || HeroSpeed < 0.0f) && anim_Walk == false && anim_axe_disableMove==false  && anim_getDemageBlood != true && anim_HeroDead != true) {
			
			if (animator.CurrentAnimation.Name != "Hero_run") {
				animator.Transition ("Hero_run", anim_Transition_Time);
			}

			if (animator.Progress >= 0.95f) {
				animator.Play ("Hero_run");

			} else if (animator.Progress < 0.95f) {
			//	Debug.Log (animator.Time + "time");
				//Debug.Log (animator.Progress + "progres");
			}
		}
	}

	void Anim_Idle()
	{
		if (HeroSpeed == 0.0f && anim_axe_disableMove==false && anim_getDemageBlood != true && anim_HeroDead != true) {
			anim_Idle = true;
			if (animator.CurrentAnimation.Name != "Hero_Idle_TakeMask") {
				animator.Transition ("Hero_Idle", anim_Transition_Time);
			}
			if (animator.Progress >= 0.95f) {
				animator.Play("Hero_Idle_TakeMask");
			} else if (animator.Progress < 0.95f) {
				//Debug.Log (animator.Time + "time");
				//Debug.Log (animator.Progress + "progres");
			}
		}
	}

	void Anim_WalkRunMode()
	{
		if (Input.GetKeyDown (KeyCode.R)) {
			if (anim_Walk == false) {
				anim_Walk = true;
				gameObject.GetComponent<Hero2DController> ().m_MaxSpeed = 5.0f;
			} else {
				anim_Walk = false;
				gameObject.GetComponent<Hero2DController> ().m_MaxSpeed = 10.0f;
			}
		}
	}

	void AnimatorController()
	{	
		AnimHeroKilled ();
		AnimGetDemage ();
		Anim_Walk ();
		Anim_Run ();
		Anim_Idle ();
		AnimAttack ();
	}

	void AnimAttack()
	{
		if (Input.GetKeyDown (KeyCode.E) && anim_getDemageBlood != true && anim_HeroDead != true) {
			anim_axe_disableMove = true;
			if (animator.CurrentAnimation.Name != "Hero_axe") {
				animator.Play("Hero_axe");
			}
		}

		if (animator.Progress >= 0.90f) {
			anim_axe_disableMove = false;
		} else if (animator.Progress < 0.95f) {
			//Debug.Log (animator.Time + "time");
			//Debug.Log (animator.Progress + "progres");
		}
	}

	void AnimGetDemage()
	{
		if (anim_getDemageBlood == true && anim_HeroDead != true) {
			if (animator.CurrentAnimation.Name != "Hero_Demaged_EffectON") {
				animator.Play ("Hero_Demaged_EffectON");
			}
		}
		if (animator.Progress >= 0.90f) {
			anim_getDemageBlood = false;
		} else if (animator.Progress < 0.95f) {
			//Debug.Log (animator.Time + " time " + animator.CurrentAnimation.Name);
			//Debug.Log (animator.Progress + " progres " + animator.CurrentAnimation.Name);
		}
	}

	void AnimHeroKilled()
	{
			//3 options, firstly death animation, next one random death idle animation
		if ( anim_HeroDead == true && anim_HeroAgony == false && animator.CurrentAnimation.Name != "Hero_Die_Static" && animator.CurrentAnimation.Name != "Hero_Die_Static_Dead_Happy" && animator.CurrentAnimation.Name != "Hero_Die_Static_Dead_Pain" ) 
		{
			animator.Play ("Hero_Die_Static");
			anim_HeroAgony = true;
		}
		if (anim_HeroAgony == true && (animator.CurrentAnimation.Name == "Hero_Die_Static" || animator.CurrentAnimation.Name == "Hero_Die_Static_Dead_Happy" || animator.CurrentAnimation.Name == "Hero_Die_Static_Dead_Pain")  && animator.Progress >=0.95f) 
		{
			float randomizeDeadIdle = UnityEngine.Random.value;
			// idle agony
			if (randomizeDeadIdle >= 0.5f) {
				if (animator.CurrentAnimation.Name != "Hero_Die_Static_Dead_Pain") {
					animator.Transition ("Hero_Die_Static_Dead_Happy", anim_Transition_Time);
				}
				if (animator.Progress >= 0.95f) {
					animator.Play ("Hero_Die_Static_Dead_Happy");
				}
			} else if (randomizeDeadIdle < 0.5f) {
				if (animator.CurrentAnimation.Name != "Hero_Die_Static_Dead_Pain") {
					animator.Transition ("Hero_Die_Static_Dead_Pain", anim_Transition_Time);
				}
				if (animator.Progress >= 0.95f) {
					animator.Play ("Hero_Die_Static_Dead_Pain");
				}
			}
		}
	}

	public void KillHero()
	{
		anim_HeroDead = true;
	}
	public void ResurectHero()
	{
		anim_HeroDead = false;
		anim_HeroAgony = false;
	}

	//SpriterPro Scripting Animation Examples code END

	//////////////////
	// LICENSE start//
	//				//
	//////////////////

	private void SwitchAnimation(int offset)
	{
		animator.Play(GetAnimation(animator, offset));
	}

	private void Transition(int offset)
	{
		animator.Transition(GetAnimation(animator, offset), TransitionTime * 1000.0f);
	}

	private void ChangeAnimationSpeed(float delta)
	{
		var speed = animator.Speed + delta;
		speed = Math.Abs(speed) < MaxSpeed ? speed : MaxSpeed * Math.Sign(speed);
		AnimatorSpeed = (float)Math.Round(speed, 1, MidpointRounding.AwayFromZero);
	}

	private void ReverseAnimation()
	{
		AnimatorSpeed *= -1;
	}

	private void PushCharacterMap()
	{
		SpriterCharacterMap[] maps = animator.Entity.CharacterMaps;
		if (maps == null || maps.Length == 0) return;
		SpriterCharacterMap charMap = animator.CharacterMap;
		if (charMap == null) charMap = maps[0];
		else
		{
			int index = charMap.Id + 1;
			if (index >= maps.Length) charMap = null;
			else charMap = maps[index];
		}

		if (charMap != null) animator.PushCharMap(charMap);
	}

	private string GetVarValues()
	{
		StringBuilder sb = new StringBuilder();

		FrameMetadata metadata = animator.Metadata;

		foreach (var entry in metadata.AnimationVars)
		{
			object value = GetValue(entry.Value);
			sb.Append(entry.Key).Append(" = ").AppendLine(value.ToString());
		}
		foreach (var objectEntry in metadata.ObjectVars)
		{
			foreach (var varEntry in objectEntry.Value)
			{
				object value = GetValue(varEntry.Value);
				sb.Append(objectEntry.Key).Append(".").Append(varEntry.Key).Append(" = ").AppendLine((value ?? string.Empty).ToString());
			}
		}

		return sb.ToString();
	}

	private object GetValue(SpriterVarValue varValue)
	{
		object value;
		switch (varValue.Type)
		{
		case SpriterVarType.Float:
			value = varValue.FloatValue;
			break;
		case SpriterVarType.Int:
			value = varValue.IntValue;
			break;
		default:
			value = varValue.StringValue;
			break;
		}
		return value;
	}

	private string GetTagValues()
	{
		FrameMetadata metadata = animator.Metadata;

		StringBuilder sb = new StringBuilder();
		foreach (string tag in metadata.AnimationTags) sb.AppendLine(tag);
		foreach (var objectEntry in metadata.ObjectTags)
		{
			foreach (string tag in objectEntry.Value) sb.Append(objectEntry.Key).Append(".").AppendLine(tag);
		}

		return sb.ToString();
	}

	private static bool GetAxisDownPositive(string axisName)
	{
		return Input.GetButtonDown(axisName) && Input.GetAxis(axisName) > 0;
	}

	private static bool GetAxisDownNegative(string axisName)
	{
		return Input.GetButtonDown(axisName) && Input.GetAxis(axisName) < 0;
	}

	private static string GetAnimation(UnitySpriterAnimator animator, int offset)
	{
		List<string> animations = animator.GetAnimations().ToList();
		int index = animations.IndexOf(animator.CurrentAnimation.Name);
		index += offset;
		if (index >= animations.Count) index = 0;
		if (index < 0) index = animations.Count - 1;
		return animations[index];
	}
		
	void AnimationState()
	{
		Debug.Log (animator.CurrentAnimation.Name);
	}
}

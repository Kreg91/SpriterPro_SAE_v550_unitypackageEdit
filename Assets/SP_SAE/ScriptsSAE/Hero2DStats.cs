using UnityEngine;
using System;
using System.Collections.Generic;
using UnityEngine.UI;
using System.Text;
using System.Collections;

public class Hero2DStats : MonoBehaviour {

	public float hpMax;
	public float hp;
	public bool godMode;
	public GameObject canvasInterfaceInfoGObj;

	public GameObject player;

	// Use this for initialization
	void Start () {
		hpMax = 100.0f;
		hp = hpMax;
		player = GameObject.FindWithTag ("Player");
		godMode = false;
		canvasInterfaceInfoGObj = GameObject.Find ("Canvas");
		canvasInterfaceInfoGObj.GetComponent<Text>().text = "Move: arrows               Hero Hit Points = " + hp + "/" + hpMax + "\nJump: space\nUse: E\nRestart: 1\nQuit: 2\nGodMode: G (is Disabled press G to Enable)";
	}
	
	// Update is called once per frame
	void Update () {
		HeroInterfaceInfo ();
		KillHero ();
		ResurectionHero ();
	}

	public	void DemageHero( float demageAmount)
	{
		if (godMode == false) {
			hp = hp - demageAmount;
		}
	}

	public void KillHero()
	{
		if (hp <= 0) {
			//Debug.Log ("dead");
			gameObject.GetComponent<Hero2DAnimControl> ().KillHero ();
			gameObject.GetComponent<Hero2DUserControl> ().moveEnabled = false;
			gameObject.GetComponent<BoxCollider2D> ().enabled = false;
			//gameObject.GetComponent<CircleCollider2D> ().enabled = false;
		}

	}

	public void ResurectionHero()
	{
		if (hp > 0 && player.GetComponent<Hero2DAnimControl>().anim_HeroAgony == true) {
			gameObject.GetComponent<Hero2DAnimControl> ().ResurectHero ();
			gameObject.GetComponent<Hero2DUserControl> ().moveEnabled = true;
			gameObject.GetComponent<BoxCollider2D> ().enabled = true;
			//gameObject.GetComponent<CircleCollider2D> ().enabled = true;
		}
	}

	public void HeroGodMode()
	{
		if (godMode == false) {
			godMode = true;
			GameObject.Find("Canvas").GetComponent<Text>().text = "Move: arrows\nJump: space\nUse: E\nRestart: 1\nQuit: 2\nGodMode: G (is Enabled press G to Disable)\n\nHero Hit Points = " + hp + "/" + hpMax;
		} else {
			godMode = false;
			GameObject.Find("Canvas").GetComponent<Text>().text = "Move: arrows\nJump: space\nUse: E\nRestart: 1\nQuit: 2\nGodMode: G (is Disabled press G to Enable)\n\nHero Hit Points = " + hp + "/" + hpMax;
		}
	}

	public void HeroInterfaceInfo()
	{
		if (godMode == true) {
			canvasInterfaceInfoGObj.GetComponent<Text> ().text = "Move: arrows\nJump: space\nUse: E\nRestart: 1\nQuit: 2\nGodMode: G (is Enabled press G to Disable)\n\nHero Hit Points = " + hp + "/" + hpMax;
		} else if (godMode == false) {
			canvasInterfaceInfoGObj.GetComponent<Text>().text = "Move: arrows\nJump: space\nUse: E\nRestart: 1\nQuit: 2\nGodMode: G (is Disabled press G to Enable)\n\nHero Hit Points = " + hp + "/" + hpMax;
		}
	}
}

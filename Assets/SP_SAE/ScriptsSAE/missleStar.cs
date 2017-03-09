using UnityEngine;
using System;
using System.Collections;

public class missleStar : MonoBehaviour {

	//Script for missleStar hazard. Includes influence on player character.

	private float xCord_missle;
	private float yCord_missle;

	private float xCord_player;
	private float yCord_player;

	private Rigidbody2D player_rig;
	private bool missleReadyToUseState;

	public float velocityHit;
	public float disableMoveTime;
	public float mDmgRestartTime;

	private GameObject player;
	private Vector2 v2_missleSpawnVelocity;
	private bool velocityChangeBool;


	void Awake()
	{	
		velocityChangeBool = true;
	}

	// Use this for initialization
	void Start () {
		xCord_missle = gameObject.transform.position.x;
		yCord_missle = gameObject.transform.position.y;
		velocityHit = 5.0f;
		disableMoveTime = 0.2f;
		missleReadyToUseState = true;
		mDmgRestartTime = 0.5f;
		player = GameObject.FindWithTag ("Player");
	}

	// Update is called once per frame
	void Update () {
		if (velocityChangeBool == true) {
			velocityChangeBool = false;
			gameObject.GetComponent<Rigidbody2D> ().velocity = new Vector2 (v2_missleSpawnVelocity.x, v2_missleSpawnVelocity.y);

		}
	}

	IEnumerator OnTriggerEnter2D(Collider2D col)
	{
		if (missleReadyToUseState == true) {
			if (col.gameObject.tag == "Player") {
				missleReadyToUseState = false;
				player_rig = col.gameObject.GetComponent<Rigidbody2D> ();
				xCord_player = col.gameObject.transform.position.x;
				yCord_player = col.gameObject.transform.position.y;

				col.gameObject.GetComponent<Hero2DUserControl> ().moveEnabled = false;

				if ((xCord_player - xCord_missle) > 0.0f) {
					player_rig.velocity = new Vector2 ( velocityHit, velocityHit);
				} else if ((xCord_player - xCord_missle) <= 0.0f)
				{
					player_rig.velocity = new Vector2 ( -velocityHit , velocityHit);
				}
				//Enable player anim
				DemagePlayer (20.0f);
				// end enable animations

				yield return new WaitForSeconds (disableMoveTime);
				col.gameObject.GetComponent<Hero2DUserControl> ().moveEnabled = true;
				yield return new WaitForSeconds (mDmgRestartTime);
				missleReadyToUseState = true;
			}
		}
	}

	void DemagePlayer(float dmg)
	{
		player.GetComponent<Hero2DAnimControl> ().anim_getDemageBlood = true;
		player.GetComponent<Hero2DStats> ().DemageHero (dmg);
	}

	public void SetVelocity(Vector2 vec2)//Use this method in missleSpawner class
	{
		v2_missleSpawnVelocity = new Vector2 (vec2.x, vec2.y);
	}
}
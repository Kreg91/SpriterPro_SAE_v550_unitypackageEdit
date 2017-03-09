using UnityEngine;
using System.Collections;

public class spikes : MonoBehaviour {

	//Script controls spikes hazards influence on player character.

	private float xCord_spikes;
	private float yCord_spikes;

	private float xCord_player;
	private float yCord_player;

	private Rigidbody2D player_rig;

	public float velocityHit;

	public float disableMoveTime;
	private bool activeTrap;
	private float trapRestartTime;

	private GameObject player;

	// Use this for initialization
	void Start () {
		xCord_spikes = gameObject.transform.position.x;
		yCord_spikes = gameObject.transform.position.y;
		velocityHit = 5.0f;
		disableMoveTime = 0.5f;
		activeTrap = true;
		trapRestartTime = 0.5f;
		player = GameObject.FindWithTag ("Player");
	}
	/*
	void OnCollisionEnter2D(Collision2D col)
	{
		
		if (col.gameObject.tag == "Player") {
			player_rig = col.gameObject.GetComponent<Rigidbody2D> ();
			xCord_player = col.gameObject.transform.position.x;
			yCord_player = col.gameObject.transform.position.y;
			//player_rig.AddForceAtPosition (new Vector2 ((xCord_player-xCord_spikes )* hitpower, (yCord_player-yCord_spikes) * hitpower), new Vector2 (xCord_spikes, yCord_spikes));
			//player_rig.velocity = new Vector2 ((xCord_player - xCord_spikes) * hitpower, (yCord_player - yCord_spikes) * hitpower);
			player_rig.velocity = new Vector2 (100.0f, 0.0f);
		}
	}
*/
	IEnumerator OnTriggerStay2D(Collider2D col)
	{
		if (activeTrap == true) {
			if (col.gameObject.tag == "Player") {
				activeTrap = false;
				player_rig = col.gameObject.GetComponent<Rigidbody2D> ();
				xCord_player = col.gameObject.transform.position.x;
				yCord_player = col.gameObject.transform.position.y;

				col.gameObject.GetComponent<Hero2DUserControl> ().moveEnabled = false;
				if ((xCord_player - xCord_spikes) > 0.0f) {
					player_rig.velocity = new Vector2 ( velocityHit, velocityHit);
				} else if ((xCord_player - xCord_spikes) <= 0.0f)
				{
					player_rig.velocity = new Vector2 ( -velocityHit , velocityHit);
				}
				DemagePlayer ();
				yield return new WaitForSeconds (disableMoveTime);
				col.gameObject.GetComponent<Hero2DUserControl> ().moveEnabled = true;
				yield return new WaitForSeconds (trapRestartTime);
				activeTrap = true;

			}
		}
	}

	void DemagePlayer()
	{
		player.GetComponent<Hero2DAnimControl> ().anim_getDemageBlood = true;
		player.GetComponent<Hero2DStats> ().DemageHero (30.0f);
	}

}

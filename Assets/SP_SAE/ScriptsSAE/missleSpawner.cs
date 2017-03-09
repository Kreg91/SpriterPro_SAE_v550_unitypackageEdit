using UnityEngine;
using System.Collections;

public class missleSpawner : MonoBehaviour {

	[TooltipAttribute("Add velocity for traps spawned missles")]
	public Vector2 v2_missleSpeed; //Set velocity in inspector as you like.
	[TooltipAttribute("Add a missle from prefabs folder")]
	public GameObject spawnMissle;

	public void SpawnMissleFunction()
	{
		GameObject missle;
		missle = (GameObject)Instantiate (spawnMissle, gameObject.transform.position, Quaternion.identity) as GameObject;
		//missle.GetComponent<missleStar> ().v2_missleSpawnVelocity = v2_missleSpeed;
		missle.GetComponent<missleStar> ().SetVelocity (v2_missleSpeed);
	}
}

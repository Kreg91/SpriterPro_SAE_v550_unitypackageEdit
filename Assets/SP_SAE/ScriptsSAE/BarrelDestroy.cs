using UnityEngine;
using System.Collections;

public class BarrelDestroy : MonoBehaviour {

	//Script for barrel hazard destruction, after its explosion.

	private GameObject expBox;
	private GameObject player;
	// Use this for initialization
	void Start () {
		expBox = gameObject.GetComponentInChildren<BarrelExplo> ().gameObject;
		player = GameObject.FindWithTag ("Player");
	}
	
	// Update is called once per frame
	void Update () {
		if (expBox.GetComponent<BarrelExplo> ().barrelDestroyState == true && expBox.GetComponent<BarrelExplo>().animator.Progress>=0.95f) {
			player.GetComponent<Hero2DUserControl> ().moveEnabled = true;
			Destroy (this.gameObject);
		}
	}
}

using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DeadZone : MonoBehaviour {

	//Script for restarting level after player character falls thro bottom of the screen.

	public GameObject deadZoneObj;
	public GameObject targetToKill;
	public Text textBox;


	// Use this for initialization
	void Start () {
		deadZoneObj = gameObject;
		targetToKill = GameObject.FindGameObjectWithTag ("Player");
		//textBox = GameObject.FindGameObjectWithTag ("TextBox").GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
		if (targetToKill.transform.position.y < deadZoneObj.transform.position.y) {
			//textBox.text = "GAME OVER";
			/*
			GameObject.FindWithTag("Player").transform.position = new Vector3 (-18f, 0f, 0.0f);
			GameObject.FindWithTag ("Player").GetComponent<Hero2DStats> ().hp = GameObject.FindWithTag ("Player").GetComponent<Hero2DStats> ().hpMax;
			GameObject.FindWithTag ("Player").GetComponent<Hero2DAnimControl> ().ResurectHero ();
			GameObject.FindWithTag ("Player").GetComponent<Hero2DUserControl> ().moveEnabled = true;
			GameObject.FindObjectOfType<WinZone>().elixirTime.GetComponent<Text>().enabled = false;
			GameObject.FindObjectOfType<WinZone>().elixirImage.GetComponent<Image>().enabled = false;
			GameObject.FindObjectOfType<WinZone>().GameTime = 0f;
			*/
			SceneManager.LoadScene (0);
		}
	
	}
}

using UnityEngine;
using System.Collections;

public class KeyBoardMenager : MonoBehaviour {

	public GameObject menuFunctionsObject;
	public GameObject Hero2DStatsGameObject;

	// Use this for initialization
	void Start () {
		menuFunctionsObject = GameObject.Find ("WIN_ZONE");
		Hero2DStatsGameObject = GameObject.FindWithTag ("Player");

	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.Alpha1))
			{
				menuFunctionsObject.SendMessage("RestartGame");
			}
		if (Input.GetKeyDown (KeyCode.Alpha2)) {
			menuFunctionsObject.SendMessage("QuitGame");
		}

		if (Input.GetKeyDown (KeyCode.G)) {
			Hero2DStatsGameObject.SendMessage ("HeroGodMode");
		}
	}



}

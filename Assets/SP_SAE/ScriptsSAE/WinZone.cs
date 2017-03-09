using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class WinZone : MonoBehaviour {

	//Checks if the player character is enough close to the winZone object.

	private GameObject winZoneObj;
	private GameObject targetToWin;
	//public Text textBox;
	public Vector2 distanceToWinVector;
	public float distanceToWin;

	private float helpXdis;
	private float helpYdis;

	private float GameTime;
	public GameObject elixirTime;
	public GameObject elixirImage;

	public bool win;

	// Use this for initialization
	void Start () {
		win = false;
		winZoneObj = gameObject;
		targetToWin = GameObject.FindGameObjectWithTag ("Player");
	
		distanceToWin = 5.0f;
		distanceToWinVector = new Vector2 (distanceToWin, distanceToWin);

		elixirTime = GameObject.Find ("ElixirTimeText");
		elixirImage = GameObject.Find ("ElixirImage");

	}

	
	// Update is called once per frame
	void Update () {
		if (win == false) {
			GameTime += Time.deltaTime;
		}

		elixirTime.GetComponent<Text>().text = "Time: " + Mathf.Ceil(GameTime);

		helpXdis = Mathf.Abs (targetToWin.transform.position.x - winZoneObj.transform.position.x);
		helpYdis = Mathf.Abs( targetToWin.transform.position.y - winZoneObj.transform.position.y);

		if ( (helpXdis < distanceToWinVector.x ) &&  (helpYdis < distanceToWinVector.y)) {
			elixirTime.GetComponent<Text>().enabled = true;
			elixirImage.GetComponent<Image>().enabled = true;
			Win ();
		}
	}

	public	void RestartGame()
	{
		Application.LoadLevel (0);
	}

	public	void QuitGame()
	{
		Application.Quit ();
	}

	void Win()
	{
		win = true;
	}

}

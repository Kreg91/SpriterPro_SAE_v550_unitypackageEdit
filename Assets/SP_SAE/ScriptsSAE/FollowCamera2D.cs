using UnityEngine;
using System.Collections;

public class FollowCamera2D : MonoBehaviour {

	//Distance in the x axis the player can move before the camera follows.
	public float xMargin = 3f;
	// in y
	public float yMargin=3f;
	//How smoothly the camera catches up with its target movement in the x axis and y axis
	public float ySmooth = 3f;
	public float xSmooth = 3f;
	//Max x and y coordinates the camera can have
	private Vector2 maxXAndY;
	//Min x and y coordinates the camera can have
	private Vector2 minXAndY;
	//Reference to the player's transform.
	public Transform player;

	private Bounds backgroundBounds;
	private Vector2 camTopLeft;
	private Vector2 camButtomRight;
	
	void Awake(){
		//get the bound for the background texture - world size
		backgroundBounds = GameObject.Find ("background").GetComponent<Renderer>().bounds;
		ViewPort ();
		//Automatically set the min and max values
		minXAndY.x = backgroundBounds.min.x - camTopLeft.x;
		maxXAndY.x = backgroundBounds.max.x - camButtomRight.x;
		//Setting up reference.
		player = GameObject.Find ("Bohater").transform;
		if (player == null)
		{
			Debug.LogError("Player object not found");
		}

	}
	void FixedUpdate()
	{
		//By defaul the target x and y coordinates of the camera are it's current x and y coordinates.
		float targetX = transform.position.x;
		float targetY = transform.position.y;
		//if the player has moved beyond the x margin...
		if (CheckXMargin ())
			//the target x coordinate should be a Lerp between the camera's current x position and the player's curren x position.
			targetX = Mathf.Lerp (transform.position.x, player.position.x, xSmooth * Time.fixedDeltaTime);
		//If the player has moved beyond the y margin...
		if(CheckYMargin ())
			//the target y coordinates should ba a Lerp between the camera's current y position and the player's current y position.
			targetY = Mathf.Lerp (transform.position.y, player.position.y, ySmooth * Time.fixedDeltaTime);
		//The target x and y coordinates should not be larger then the maximum or smaller then the minimum.
		targetX = Mathf.Clamp (targetX, minXAndY.x, maxXAndY.x);
		targetY = Mathf.Clamp (targetY, minXAndY.y, maxXAndY.y);
		//Set the camera's position to the target position with the same z component.
		transform.position = new Vector3 (targetX, targetY, transform.position.z);

	}
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	bool CheckXMargin(){
		//Return true if the distance between the camera and the player in the x axis is greater then the x margin
		return Mathf.Abs (transform.position.x - player.position.x) > xMargin;
	}
	bool CheckYMargin(){
		//Return true if the distance between the camera and the player in the y axis is greater then the x margin
		return Mathf.Abs (transform.position.y - player.position.y) > yMargin;
	}
	void ViewPort(){
		//Get the viewable bounds of the camera in world coordinates
		camTopLeft = GetComponent<Camera>().ViewportToWorldPoint (new Vector3 (0, 0, 0));
		camButtomRight = GetComponent<Camera>().ViewportToWorldPoint (new Vector3 (1, 1, 0));
	}
}

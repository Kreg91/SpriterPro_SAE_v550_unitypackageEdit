using UnityEngine;
using System.Collections;

public class SwitchPlatform : MonoBehaviour {

	// This script is designed to control movement of a floating platform turned on and off by a switch.
	//  Use the variables to set proper checkpoints in the scene.
	// Check "Switches" object in the scene for further references.

	public bool checkpoint0;
	public bool checkpoint17;
	public bool platformOn;
	public float platformX;
	public float platformTime;
	public float interpolationSpeed;
	public float startPoint;
	public float endPoint;

	// Use this for initialization
	void Start () {
		checkpoint0 = true;
		checkpoint17 = false;
		platformOn = false;
		interpolationSpeed = 1f / 4f;
	}
	
	// Update is called once per frame
	void Update () {

		if (platformOn == true && checkpoint0 == true) {
			platformTime += Time.deltaTime * interpolationSpeed ;
			if (platformTime >= 1.0f) {
				checkpoint0 = false;
				checkpoint17 = true;
				platformTime = 0.0f;
			}
		}

		if (platformOn == true && checkpoint17 == true) {
			platformTime += Time.deltaTime * interpolationSpeed ;
			if (platformTime >= 1.0f) {
				checkpoint0 = true;
				checkpoint17 = false;
				platformTime = 0.0f;
			}
		}
	

	
		if ( checkpoint0 == true && platformOn == true ) {
			platformX = Mathf.LerpUnclamped (startPoint, endPoint, platformTime);
			gameObject.transform.localPosition = new Vector3 (platformX, gameObject.transform.localPosition.y, gameObject.transform.position.z);
			//checkpoint0 = false;
		} 

		if ( checkpoint17 == true && platformOn == true ) {
			platformX = Mathf.LerpUnclamped (endPoint, startPoint, platformTime);
			gameObject.transform.localPosition = new Vector3 (platformX, gameObject.transform.localPosition.y, gameObject.transform.position.z);
			//checkpoint0 = false;
		} 



	}

	void PlatformOnOff()
	{
		if (platformOn == false) {
			platformOn = true;
		} else {
			platformOn = false;
		}
	}

}

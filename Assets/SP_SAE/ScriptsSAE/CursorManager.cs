using UnityEngine;
using System.Collections;

public class CursorManager: MonoBehaviour 
{
	public bool cursorVisible;
	// Use this for initialization
	void Start () 
	{
		Cursor.visible = false;
		cursorVisible = false;
	}
	void Update()
	{
		if (cursorVisible == true) {
			Cursor.visible = true;
		} else {
			Cursor.visible = false;
		}
	}
}
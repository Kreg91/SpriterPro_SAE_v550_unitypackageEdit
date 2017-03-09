//Script based on UnityStandardAssets
using System;
using UnityEngine;

[RequireComponent(typeof (Hero2DController))]
public class Hero2DUserControl : MonoBehaviour {

	private Hero2DController m_Character;
	private bool m_Jump;
	public bool moveEnabled = true;
	public float h;

	private void Awake()
	{
		m_Character = GetComponent<Hero2DController>();
		h = 0;
	}
		
	private void Update()
	{
		if (!m_Jump)
		{
			// Read the jump input in Update so button presses aren't missed.
			m_Jump = Input.GetButtonDown("Jump");
		}
	}


	private void FixedUpdate()
	{
		// Read the inputs.
		h = Input.GetAxis ("Horizontal");

		if (moveEnabled == true) {
			
			m_Character.Move(h, m_Jump);
		}
		m_Jump = false;
	}
}

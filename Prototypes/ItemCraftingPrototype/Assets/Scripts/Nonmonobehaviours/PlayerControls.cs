using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

[System.Serializable]
	public class MovementControls
	{
		public static MovementControls instance;
		public KeyCode moveForward = KeyCode.W;
		public KeyCode moveLeft = KeyCode.A;
		public KeyCode moveBackward = KeyCode.S;
		public KeyCode moveRight = KeyCode.D;

		public KeyCode jump = KeyCode.Space;

		public MovementControls()
		{
			instance = this;
		}
	}
	
	[System.Serializable]
	public class ActionControls
	{
		public static ActionControls instance;
		public KeyCode select = KeyCode.Space;

		public KeyCode unselect = KeyCode.Backspace;

		public KeyCode useItem = KeyCode.Mouse0;

		public ActionControls()
		{
			instance = this;
		}
	}

[System.Serializable]
public class PlayerControls
{
	public static PlayerControls 	instance;

	[SerializeField]
	MovementControls _movement;
	[SerializeField]
	ActionControls _action;
	
	public PlayerControls()
	{
		instance = this;
		
	}
	
}

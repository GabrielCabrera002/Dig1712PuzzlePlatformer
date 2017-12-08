using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class SystemSettings : MonoBehaviour 
{
	public static SystemSettings instance;
	public PlayerControls controls;

	
	void Awake()
	{
		instance = this;
	}
}

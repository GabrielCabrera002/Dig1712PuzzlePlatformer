using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class GameController : MonoBehaviour 
{
	public static GameController instance;
	SystemSettings systemSettings;
	
	void Awake()
	{
		instance = this;
	}

	// Use this for initialization
	void Start () 
	{
		systemSettings = SystemSettings.instance;
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}
	
}

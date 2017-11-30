using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class PlayerController : ObservableMonoBehaviour 
{
	#region Submodules
	[SerializeField]
	PlayerMovement movement;
	#endregion

	#region Fields
	[SerializeField]
	new public Transform transform { get; set; }
	new public Renderer renderer   { get; set; }
	new public Collider collider   { get; set; }
	new public Rigidbody rigidbody { get; set; }
	#endregion
	
	#region Properties
	public bool isMoving {get;}
	#endregion

	#region Methods
	void Awake()
	{
		SetupBaseComponents();
		movement.Init(this);
	}
	// Use this for initialization
	void Start () 
	{
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		//movement.Execute(); // for some reason, the movement is glitchy when called in Update
	}

	void FixedUpdate()
	{
		movement.Execute();
	}

    void SetupBaseComponents()
	{
		transform = 	GetComponent<Transform>();
		collider = 		GetComponent<Collider>();
		renderer = 		GetComponent<Renderer>();
		rigidbody = 	GetComponent<Rigidbody>();
	}

    #endregion
}

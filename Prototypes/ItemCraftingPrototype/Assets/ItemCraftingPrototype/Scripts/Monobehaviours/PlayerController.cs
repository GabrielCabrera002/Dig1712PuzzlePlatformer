using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class ItemEvent : UnityEvent<ItemInfo> {}
public class PlayerController : ObservableMonoBehaviour 
{
	#region Events
	public ItemEvent AddedToInventory 
	{ 
		get { return inventory.AddedToInventory; }
	}
	public ItemEvent RemovedFromInventory 
	{
		get { return inventory.RemovedFromInventory; } 
	}
	#endregion

	#region Submodules
	[SerializeField]
	PlayerMovement movement;
	PlayerItemInteractionHandler interactionHandler = new PlayerItemInteractionHandler();
	#endregion

	#region Fields
	static PlayerController inst;

	[SerializeField]
	PP_Inventory _inventory;
	#endregion
	
	#region Properties
	
	public static PlayerController instance 
	{ 
		get { return inst; } 
		protected set { inst = value; }
	}
	new public Collider collider   { get; set; }
	new public Rigidbody rigidbody { get; set; }
	public bool isMoving 
	{
		get { return movement.isActive; }
	}

	public PP_Inventory inventory
	{
		get { return _inventory; }
		protected set { _inventory = value; }
	}
	#endregion

	#region Methods
	new void Awake()
	{
		base.Awake();
		instance = this;
		SetupBaseComponents();
		movement.Init(this);
		interactionHandler.Init(this);
		inventory.Init();
		
	}
	// Use this for initialization
	new void Start () 
	{
		base.Start();
	}
	
	// Update is called once per frame
	new void Update () 
	{
		//movement.Execute(); // for some reason, the movement is glitchy when called in Update
	}

	new void FixedUpdate()
	{
		base.FixedUpdate();
		movement.Execute();
	}

    void SetupBaseComponents()
	{
		collider = 		GetComponent<Collider>();
		rigidbody = 	GetComponent<Rigidbody>();
	}

	void StopMoving()
	{
		movement.isActive = false;
	}

	public void AddToInventory(ItemInfo item)
	{
		inventory.Add(item);
	}

	public void RemoveFromInventory(ItemInfo item)
	{
		inventory.Remove(item);
	}


    #endregion
}

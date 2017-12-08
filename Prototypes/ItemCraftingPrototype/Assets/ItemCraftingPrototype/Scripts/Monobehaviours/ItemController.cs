using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class ItemController : ObservableMonoBehaviour 
{
	public int itemId; // decides which item from the database this will have
	[SerializeField]
	[Header("Item Info set programmatically based on ID")]
	protected ItemInfo item;
	PlayerController player;
	new public SphereCollider collider { get; private set; }
	public Vector3 rotationSpeeds;

	public bool isCollectable = true;

	new void Awake()
	{
		base.Awake();
		collider = GetComponent<SphereCollider>();
	}

	new void Start()
	{
		player = GameController.instance.player;
		FetchItemFromDatabase();
		SubscribeToEvents();
	}

	new void Update()
	{
		HandleRotation();
	}

	protected virtual void HandleRotation()
	{
		transform.Rotate(rotationSpeeds * Time.deltaTime);
	}
	
	public ItemInfo PickUp()
	{
		Invoke("Destroy", Time.deltaTime);
		return item;
	}

	void FetchItemFromDatabase()
	{
		item = ItemDatabase.instance.GetItem(itemId);
	}

	void SubscribeToEvents()
	{
		player.AddedToInventory.AddListener(OnAddedToInventory);
	}

	void OnAddedToInventory(ItemInfo item)
	{
		if (item == this.item)
			Destroy(this.gameObject);
	}
}

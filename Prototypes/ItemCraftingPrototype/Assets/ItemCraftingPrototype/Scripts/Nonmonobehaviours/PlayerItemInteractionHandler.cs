using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class PlayerItemInteractionHandler 
{
	#region Fields
	PlayerController player;
	#endregion

	#region Properties

	#endregion

	#region Methods
	public void Init(PlayerController player)
	{
		this.player = player;
		SubscribeToEvents();
	}

	void OnTriggerEnter(Collider other)
	{
		// if the other object is a collectable item, collect it
		ItemController item = other.GetComponent<ItemController>();

		bool collectableItem = (item != null && item.isCollectable);

		if (collectableItem)
			player.AddToInventory(item.PickUp());
		
	}

	void SubscribeToEvents()
	{
		player.mBEvents.OnTriggerEnter.AddListener(OnTriggerEnter);
	}
	#endregion
	
}

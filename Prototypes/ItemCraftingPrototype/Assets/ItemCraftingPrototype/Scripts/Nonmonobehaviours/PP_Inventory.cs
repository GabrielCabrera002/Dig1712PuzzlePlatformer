using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System;

[System.Serializable]
public class PP_Inventory : IInventory
{
	#region Events
	public ItemEvent AddedToInventory { get; set; }
	public ItemEvent RemovedFromInventory { get; set; }
	#endregion

	#region Fields
	[SerializeField]
	bool _infiniteSpace = false;
	public int capacity;
	const int INFINITE = -1;

	#region Backing Fields
	[SerializeField]
	List<ItemInfo> _contents = new List<ItemInfo>();
	#endregion

	#region The contents categorized in separate lists
	[Header("___________________________")]
	[Header("Categorized Lists")]
	[SerializeField]
	List<ItemInfo> baseMaterials = new List<ItemInfo>();
	[SerializeField]
	List<ItemInfo> usableItems = new List<ItemInfo>();
	[SerializeField]
	List<ItemInfo> healingItems = new List<ItemInfo>();
	[SerializeField]
	List<ItemInfo> machineItems = new List<ItemInfo>();
	[SerializeField]
	List<ItemInfo> ammo = new List<ItemInfo>();

	List<List<ItemInfo>> categorizedLists = new List<List<ItemInfo>>();
	//^ Helps when repeating the same operations on each of the categorized lists

	#endregion
	#endregion

	#region Properties
	public List<ItemInfo> items
	{
		get { return _contents; }
		protected set { _contents = value; }
	}
	public bool infiniteSpace
	{
		get { return _infiniteSpace; }
	}
	#endregion

	#region Methods 

	#region Init/Constructors
	public PP_Inventory() : base()
	{
		SetupEvents();
		SetupCategorizedLists();

		if (!infiniteSpace)
			items.Capacity = capacity;

	}

	public PP_Inventory(IEnumerable<IItem> collection) : this()
	{
		int collectionCount = 0;

		foreach (IItem item in collection)
		{
			Add(item as ItemInfo);
			collectionCount++;
		}

		if (!infiniteSpace && collectionCount > capacity)
			Debug.LogWarning("Could not copy over all items from the passed collection; capacity exceeded.");
		
	}

	public void Init()
	{
		// Setting the capacity doesn't work as it should in a regular constructor, 
		// hence this function.
		if (!infiniteSpace)
			items.Capacity = capacity;
	}

	#endregion

	#region Item Adding/Removal functions
	/// <summary>
	/// If the inventory has the space for it, adds the item passed.
	/// </summary>
	/// <param name="item">The item passed.</param>
	/// <returns>True if the item was added, false otherwise.</returns>
	public bool Add(ItemInfo item)
	{
		if (infiniteSpace || items.Count < items.Capacity)
		{
			items.Add(item);

			// update the lists for the categories
			if (item.categories.Contains(Category.baseMaterial))
				baseMaterials.Add(item);
			
			if (item is IUsableItem)
				usableItems.Add(item);

			if (item.categories.Contains(Category.healing))
				healingItems.Add(item);

			if (item.categories.Contains(Category.ammo))
				ammo.Add(item);

			if (item.categories.Contains(Category.machine))
				machineItems.Add(item);

			AddedToInventory.Invoke(item);
			return true;
		}

		return false;
		
	}

    public bool Remove(ItemInfo item)
    {
		bool removed = Utils.RemoveItemFromList(items, item);

		if (removed)
		{
			if (item.categories.Contains(Category.baseMaterial))
				Utils.RemoveItemFromList(baseMaterials, item);
				
			if (item is IUsableItem)
				Utils.RemoveItemFromList(usableItems, item);

			if (item.categories.Contains(Category.healing))
				Utils.RemoveItemFromList(healingItems, item);

			if (item.categories.Contains(Category.ammo))
				Utils.RemoveItemFromList(ammo, item);

			if (item.categories.Contains(Category.machine))
				Utils.RemoveItemFromList(machineItems, item);

			RemovedFromInventory.Invoke(item);
		}
		
		return removed;
    }

    public bool Contains(ItemInfo item)
    {
        return items.Contains(item);
    }

	/// <summary>
	/// Returns how many of the passed item are in this inventory.
	/// </summary>
	/// <param name="item"></param>
	/// <returns></returns>
	public int CountOf(ItemInfo item)
	{
		int howMany = 0;

		foreach (ItemInfo currentItem in items)
			if (currentItem.Equals(item))
				howMany++;
		
		return howMany;
	}

	public void Clear()
	{
		items.Clear();

		foreach (List<ItemInfo> list in categorizedLists)
			list.Clear();
	}

	#endregion

	#region Sorting methods
    public void Sort(int index, int count, IComparer<ItemInfo> comparer)
    {
        items.Sort(index, count, comparer);

		foreach (List<ItemInfo> list in categorizedLists)
			list.Sort(index, count, comparer);
    }

    public void Sort(Comparison<ItemInfo> comparison)
    {
        items.Sort(comparison);

		foreach (List<ItemInfo> list in categorizedLists)
			list.Sort(comparison);
    }

    public void Sort()
    {
		items.Sort();

		foreach (List<ItemInfo> list in categorizedLists)
			list.Sort();

    }

    public void Sort(IComparer<ItemInfo> comparer)
    {
        items.Sort(comparer);

		foreach (List<ItemInfo> list in categorizedLists)
			list.Sort(comparer);
    }

	#endregion

	#region Helper functions

	void SetupCategorizedLists()
	{
		categorizedLists.Add(baseMaterials);
		categorizedLists.Add(usableItems);
		categorizedLists.Add(healingItems);
		categorizedLists.Add(machineItems);
		categorizedLists.Add(ammo);

	}

	void SetupEvents()
	{
		AddedToInventory = new ItemEvent();
		RemovedFromInventory = new ItemEvent();
	}

	#endregion

	#endregion
}

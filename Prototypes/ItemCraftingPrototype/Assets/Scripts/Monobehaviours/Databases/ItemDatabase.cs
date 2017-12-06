using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Linq;

class ItemGroup
{

}
public class ItemDatabase : MonoBehaviour 
{

	public static ItemDatabase instance;
	public ItemInfo[] baseMaterials;
	public CraftableItemInfo[] craftableItems;
	public ItemInfo[] etc;

	List<ItemInfo> allItems = new List<ItemInfo>();
	
	void Awake()
	{
		instance = this;
		
		RegisterAllItems();
		CheckItemIds();
		
	}

	/// <summary>
	/// Gets an item from this database based on the id passed.
	/// </summary>
	/// <param name="id">The id passed.</param>
	/// <returns>A copy of the ItemInfo if found. Null otherwise. </returns>
	public ItemInfo GetItem(int id)
	{
		for (int i = 0; i < allItems.Count; i++)
			if (allItems[i].id == id)
				return new ItemInfo(allItems[i]);
		
		Debug.LogWarning("Could not find an item with id " + id + " in database.");
		return null;
	}

	void RegisterAllItems()
	{		
		foreach (ItemInfo item in baseMaterials)
			allItems.Add(item);
		
		foreach (ItemInfo item in craftableItems)
			allItems.Add(item);

		foreach (ItemInfo item in etc)
			allItems.Add(item);

	}

	void CheckItemIds()
	{
		// looks through all items, and throws an exception if any two items 
		// share an id number

		allItems = new List<ItemInfo>(allItems.OrderBy(item => item.id));

		for (int i = 1; i < allItems.Count; i++)
		{
			if (allItems[i].id == allItems[i - 1].id)
			{
				ItemInfo firstItem = allItems[i - 1];
				ItemInfo secondItem = allItems[i];

				string messageFormat = "The items named {0} and {1} have the same id.";
				throw new System.ApplicationException(string.Format(messageFormat, 
																	firstItem.name, 
																	secondItem.name));
			}
		}
	}

	
}

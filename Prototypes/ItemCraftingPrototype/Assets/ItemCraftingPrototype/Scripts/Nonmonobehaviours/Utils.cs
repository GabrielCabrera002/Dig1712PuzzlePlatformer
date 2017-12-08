using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public static class Utils 
{
	/*
	Contains useful utility and extension methods. 
	*/

	public static bool RemoveItemFromList(IList<ItemInfo> list, ItemInfo toRemove)
	{
		/*
		The built-in list Remove() function doesn't use the Equals() method like it should
		with the ItemInfo class, hence this function to bandaid microsoft's negligence. 

		Yes, I am salty.
		*/

		ItemInfo item;

		for (int i = 0; i < list.Count; i++)
		{
			item = list[i];

			if (item.Equals(toRemove))
			{
				list.RemoveAt(i);
				return true;
			}
		}

		return false;
	}

}

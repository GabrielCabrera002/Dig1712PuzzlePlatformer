using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

 interface IInventory
{
	List<ItemInfo> items { get; }
	bool Add(ItemInfo item);
	bool Remove(ItemInfo item);
	bool Contains(ItemInfo item);

	int CountOf(ItemInfo item);

	void Sort(int index, int count, IComparer<ItemInfo> comparer);
	void Sort(System.Comparison<ItemInfo> comparison);
	void Sort();
	void Sort(IComparer<ItemInfo> comparer);
	
}

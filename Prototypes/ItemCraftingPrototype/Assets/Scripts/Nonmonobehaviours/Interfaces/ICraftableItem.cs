using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public interface ICraftableItem : IItem
{
    List<ItemInfo> matsToCraft { get; }
    List<Effect> effects { get; }
	
}

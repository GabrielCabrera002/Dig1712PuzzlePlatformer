using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

[System.Serializable]
public class CraftableItemInfo : ItemInfo, ICraftableItem
{
	#region Backing Fields
	[SerializeField]
	List<ItemInfo> 					_matsToCraft;
	[SerializeField]
	List<Effect> 					_effects;
	#endregion

    public List<ItemInfo> 			matsToCraft 
	{ 
		get { return _matsToCraft; } 
		protected set { _matsToCraft = value; }
	}

    public List<Effect> 			effects 
	{
		get { return _effects; } 
		protected set { _effects = value; }
	}

	public CraftableItemInfo(CraftableItemInfo item) : base(item)
	{
		this.matsToCraft = new List<ItemInfo>(item.matsToCraft);
		this.effects = new List<Effect>(item.effects);
	}
}

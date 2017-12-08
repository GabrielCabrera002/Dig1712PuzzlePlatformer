using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

[System.Serializable]
public class RecipeInfo : ItemInfo, System.IEquatable<RecipeInfo>
{

	#region Fields
	ItemDatabase itemDatabase;

	#region Backing Fields
	[SerializeField]
	int _resultID; // set this in the inspector to decide what this recipe will be for
	[SerializeField]
	int[] _materialIDs; // set this in the inspector to decide what the mats are

	[Header("Generated dynamically")]
	[SerializeField]
	ItemInfo _result;
	[SerializeField]
	List<ItemInfo> _materials;
	#endregion

	#endregion

	#region Properties

	public int resultID
	{
		get { return _resultID; }
		protected set { _resultID = value; }
	}

	public int[] materialIDs
	{
		get { return _materialIDs; }
		protected set { _materialIDs = value; }
	}

	public ItemInfo result 
	{
		get { return _result; }
		protected set { _result = value; }
	}

	public List<ItemInfo> materials 
	{
		get { return _materials; }
		protected set { _materials = value; }
	}

	#endregion

	#region Methods
	public RecipeInfo(RecipeInfo toCopy) : base(toCopy as ItemInfo)
	{
		this.resultID = 	toCopy.resultID;
		this._materialIDs = toCopy.materialIDs;
		this.result = 		toCopy.result;
		this.materials = 	toCopy.materials;
	}
	public void Init()
	{
		itemDatabase = ItemDatabase.instance;

		// Find what item this recipe is to help craft, based on the ID passed in the inspector
		ItemInfo itemToCraft = itemDatabase.GetItem(resultID);

		if (itemToCraft == null)
			throw new System.ApplicationException("Item with ID " + id + " is not in the item database.");
	
		result = itemToCraft;

		ItemInfo mat;

		// now find the materials
		foreach (int matId in materialIDs)
		{
			mat = itemDatabase.GetItem(matId);

			if (mat == null)
				throw new System.ApplicationException("Item with ID " + id + " is not in the item database.");

			materials.Add(mat);
		}
	}

	/// <summary>
	/// Returns true or false depending on whether the passed item is one of the
	/// materials this recipe asks for.
	/// </summary>
	/// <param name="item"></param>
	/// <returns></returns>
	public bool Contains(ItemInfo item)
	{
		foreach (ItemInfo mat in materials)
			if (mat.Equals(item))
				return true;

		return false;
	}

	/// <summary>
	/// Returns how many of the passed item are in this recipe's list of materials.
	/// </summary>
	/// <param name="item"></param>
	/// <returns></returns>
	public int CountOf(ItemInfo item)
	{
		int howMany = 0;

		foreach (ItemInfo mat in materials)
			if (mat.Equals(item))
				howMany++;

		return howMany;
	}

	public bool Equals(RecipeInfo other)
	{
		// If both recipes share the same name, id, result, and materials, they're
		// the same recipe.

		bool sameBaseDetails = 		base.Equals(other as IItem);
		bool sameResult = 			this.result.Equals(other.result);
		bool sameMaterialCount = 	this.materials.Count == other.materials.Count;

		if (sameMaterialCount)
			for (int i = 0; i < this.materials.Count; i++)
				if (!this.materials[i].Equals(other.materials[i]))
					return false;
				// Don't worry about the materials in either recipe being ordered the 
				// same way.
			
		else 
			return false;

		bool sameMaterials = true;

		return (sameBaseDetails && sameResult && sameMaterials);
	}

	new public RecipeInfo Copy()
	{
		return new RecipeInfo(this);
	}

	#endregion
	
}

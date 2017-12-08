using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Linq;

[System.Serializable]
public class ItemInfo : IItem, System.IEquatable<IItem>, System.IComparable<ItemInfo>
{
	#region Backing Fields
	[SerializeField]
	string _name = "";
	[SerializeField]
	string _description = "";
	[SerializeField]
	Sprite _sprite;
	[SerializeField]
	Mesh _mesh;
	[SerializeField]
	List<Category> _categories = new List<Category>();

	[SerializeField]
	int _id = 0;

	[SerializeField]
	Effect _effect;
	#endregion

	#region Properties
	public string name
	{
		get { return _name; }
		set { _name = value; }
	}

	public string description
	{
		get { return _description; }
		set { _description = value; }
	}

	public Sprite sprite
	{
		get { return _sprite; }
		set { _sprite = value; }
	}

	public Mesh mesh
	{
		get { return _mesh; }
		set { _mesh = value; }
	}

	public List<Category> categories 
	{
		get { return _categories; }
		set { _categories = value; }
	}

	public int id
	{
		get { return _id; }
	}

	public Effect effect
	{
		get { return _effect; }
		set { _effect = value; }
	}

	#endregion

	#region Methods

	public ItemInfo(ItemInfo item)
	{
		this.name = item.name;
		this.description = item.description;
		this.sprite = item.sprite;
		this.mesh = item.mesh;
		this.categories = new List<Category>(item.categories);
		this._id = item.id;
	}

	public bool Equals(IItem item)
	{
		bool sameNameAndDesc = item.name == this.name && item.description == this.description;

		bool sameSprites = true;
		bool sameMeshes = false;

		// not all items have both a sprite and a mesh attached to them, so...
		if (item.sprite != null && this.sprite != null)
			sameSprites = item.sprite.name == this.sprite.name;
		else if (item.sprite == null && this.sprite == null)
			sameSprites = true;


		if (item.mesh != null && this.mesh != null)
			sameMeshes = item.mesh.name == this.mesh.name;
		else if (item.mesh == null && this.mesh == null)
			sameMeshes = true;

		bool sameGraphics = sameSprites && sameMeshes;
		
		bool sameCategories = Enumerable.SequenceEqual(this.categories.OrderBy(t => t), 
														item.categories.OrderBy(t => t));

		if (sameNameAndDesc && sameGraphics && sameCategories)
			return true;
		else
			return false;
	}

	public virtual ItemInfo Copy()
	{
		return new ItemInfo(this);
	}

    public virtual int CompareTo(ItemInfo other)
    {
		// compare based on id number
        if (this.id > other.id)
			return 1;
		
		if (this.id < other.id)
			return -1;

		return 0;
    }


	#endregion
}

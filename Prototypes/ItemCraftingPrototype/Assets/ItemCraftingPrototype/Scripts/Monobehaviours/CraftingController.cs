using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class CraftingController : MonoBehaviour 
{
	#region Fields
	public static CraftingController instance;
	PlayerController player;
	ItemDatabase itemDatabase;
	RecipeDatabase recipeDatabase;

	#region Backing Fields
	[SerializeField]
	List<int> _unlockedRecipeIds = new List<int>();
	[Header("Set dynamically")]
	[SerializeField]
	List<RecipeInfo> _unlockedRecipes = new List<RecipeInfo>();
	#endregion

	#endregion

	#region Properties

	#region For Backing Fields
	public List<RecipeInfo> unlockedRecipes 
	{
		get { return _unlockedRecipes; }
		protected set { _unlockedRecipes = value; }
	}
	#endregion

	#endregion

	#region Methods

	#region Initialization
	void Awake()
	{
		instance = this;
	}

	// Use this for initialization
	void Start () 
	{
		itemDatabase = ItemDatabase.instance;
		recipeDatabase = RecipeDatabase.instance;
		InitUnlockedRecipes();
	}

	#endregion

	/// <summary>
	/// Using the materials in the passed inventory, crafts and returns the item the
	/// passed recipe is for.
	/// </summary>
	/// <param name="recipe"></param>
	/// <param name="inventory"></param>
	/// <returns>The recipe's result if the inventory has the mats. Null otherwise.</returns>
	public ItemInfo Craft(RecipeInfo recipe, PP_Inventory inventory)
	{
		ItemInfo itemToCraft = recipe.result;
		
		// check if the passed inventory has the materials for the recipe
		foreach (ItemInfo material in recipe.materials)
		{
			if (inventory.CountOf(material) < recipe.CountOf(material))
			{
				string messageFormat = "Could not craft {0}; the inventory doesn't have enough {1}s.";
				Debug.Log(string.Format(messageFormat, itemToCraft.name, material.name));
				return null;
			}
		}

		// Use up the materials
		foreach (ItemInfo material in recipe.materials)
			inventory.Remove(material);

		// and voila! Item-crafting success!
		Debug.Log("Successfully crafted a(n) " + itemToCraft.name);
		return itemToCraft.Copy();

		
	}

	public void UnlockRecipe(int id)
	{
		RecipeInfo recipe = recipeDatabase.GetRecipe(id);

		if (recipe == null)
			throw new System.ArgumentException("Recipe database does not have an item with id " + id);
		
		if (unlockedRecipes.Contains(recipe))
			Debug.Log("Recipe with id " + id + " is already unlocked.");
		else 
		{
			unlockedRecipes.Add(recipe);
			if (!_unlockedRecipeIds.Contains(id))
				_unlockedRecipeIds.Add(id);
		}
		
	}

	#region Helper functions
	void InitUnlockedRecipes()
	{
		// To unlock the recipes that are meant to be unlocked at the start of the 
		// scene.
		foreach (int id in _unlockedRecipeIds)
			UnlockRecipe(id);
		
	}
	#endregion

	#endregion
}

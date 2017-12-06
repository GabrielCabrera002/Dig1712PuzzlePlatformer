using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class RecipeDatabase : MonoBehaviour 
{
	#region Fields
	public static RecipeDatabase instance;
	ItemDatabase itemDatabase;
	[SerializeField]
	public RecipeInfo[] recipes; // set these manually in the inspector
	#endregion

	#region Methods
	void Awake()
	{
		instance = this;
	}

	void Start()
	{
		itemDatabase = ItemDatabase.instance;
		foreach (RecipeInfo recipe in recipes)
			recipe.Init();

	}

	public RecipeInfo GetRecipe(int id)
	{
		foreach (RecipeInfo recipe in recipes)
			if (recipe.id == id)
				return new RecipeInfo(recipe);

		Debug.LogWarning("Could not find recipe with id " + id + " in the recipe database.");
		return null;
	}

	public bool Contains(RecipeInfo recipe)
	{
		for (int i = 0; i < recipes.Length; i++)
			if (recipes[i].Equals(recipe))
				return true;

		return false;
	}
	public bool ContainsRecipeFor(ItemInfo item)
	{
		foreach (RecipeInfo recipe in recipes)
			if (recipe.result.Equals(item))
				return true;

		return false;
	}

	#endregion

}

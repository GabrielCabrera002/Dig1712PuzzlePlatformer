using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class CraftingTester : MonoBehaviour 
{
	#region Fields
	GameController gameController;
	PlayerController player;
	CraftingController craftingController;
	ItemDatabase itemDatabase;
	RecipeDatabase recipeDatabase;
	
	public int recipeId;
	[Header("Set dynamically")]
	[SerializeField]
	RecipeInfo recipe;

	Button craftingButton;
	#endregion

	#region Methods
	// Use this for initialization
	void Start () 
	{
		gameController = 		GameController.instance;
		player = 				gameController.player;
		craftingController = 	CraftingController.instance;
		itemDatabase = 			ItemDatabase.instance;
		recipeDatabase = 		RecipeDatabase.instance;
		recipe = 				recipeDatabase.GetRecipe(recipeId);

		craftingButton = 		GetComponent<Button>();
		craftingButton.onClick.AddListener(Craft);
		
	}
	
	void Craft()
	{
		ItemInfo craftedItem = craftingController.Craft(recipe, player.inventory);

		if (craftedItem != null)
		{
			player.AddToInventory(craftedItem);
			Debug.Log("Successfully crafted a " + craftedItem.name);
		}
		else 
			Debug.Log("Failed to craft a " + recipe.result.name);
		
	}

	#endregion
	
}

using System.Collections.Generic;
using UnityEngine;

public class RecipeManager : MonoBehaviour
{
    public List<Recipe> recipes;
    public Recipe selectedRecipe;

    void Start()
    {
        if (recipes.Count > 0)
        {
            selectedRecipe = recipes[Random.Range(0, recipes.Count)];
            Debug.Log("🍽 Seçilen yemek: " + selectedRecipe.dishName);
        }
        else
        {
            Debug.LogError("❌ Tarif listesi boş! Lütfen tarifleri ekleyin.");
        }
    }
}

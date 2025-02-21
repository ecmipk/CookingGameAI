using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngredientSpawner : MonoBehaviour
{
    public Transform spawnPoint;
    public CookingPot cookingPot;
    public float spawnDelay = 1.5f;

    private RecipeManager recipeManager;

    void Start()
    {
        recipeManager = FindFirstObjectByType<RecipeManager>();
        if (recipeManager == null)
        {
            Debug.LogError("❌ RecipeManager bulunamadı!");
            return;
        }

        if (cookingPot == null)
        {
            Debug.LogError("❌ CookingPot atanmadı!");
            return;
        }

        Debug.Log("✅ IngredientSpawner başlatıldı, malzemeler spawn edilecek...");
        StartCoroutine(SpawnIngredients());
    }

    IEnumerator SpawnIngredients()
    {
        if (recipeManager.selectedRecipe == null)
        {
            Debug.LogError("❌ Seçilen tarif NULL!");
            yield break;
        }

        Debug.Log("🍽 Malzemeler spawn edilmeye başlıyor...");
        foreach (GameObject ingredientPrefab in recipeManager.selectedRecipe.ingredients)
        {
            if (ingredientPrefab == null)
            {
                Debug.LogError("❌ Bir malzeme NULL! Tarifi kontrol et.");
                continue;
            }

            Debug.Log("🥕 Spawn ediliyor: " + ingredientPrefab.name);
            GameObject ingredient = Instantiate(ingredientPrefab, spawnPoint.position, Quaternion.identity);
            Rigidbody rb = ingredient.GetComponent<Rigidbody>();

            if (rb == null)
            {
                rb = ingredient.AddComponent<Rigidbody>();
            }

            yield return new WaitForSeconds(spawnDelay);
        }

        Debug.Log("✅ Tüm malzemeler spawn edildi.");
    }
}

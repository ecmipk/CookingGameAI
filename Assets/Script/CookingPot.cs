using System.Collections.Generic;
using UnityEngine;

public class CookingPot : MonoBehaviour
{
    public Transform dishSpawnPoint;
    private List<GameObject> collectedIngredients = new List<GameObject>();
    private RecipeManager recipeManager;

    void Start()
    {
        recipeManager = FindAnyObjectByType<RecipeManager>();

        if (recipeManager == null)
        {
            Debug.LogError("❌ RecipeManager bulunamadı!");
        }

        if (dishSpawnPoint == null)
        {
            Debug.LogError("❌ Dish Spawn Point atanmadı! Inspector'da kontrol edin.");
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ingredient"))  
        {
            Debug.Log("✅ Malzeme tencereye düştü: " + other.gameObject.name);

            // ✅ Eğer zaten bu malzeme eklenmişse tekrar ekleme
            if (collectedIngredients.Contains(other.gameObject))
            {
                Debug.LogWarning("⚠️ Aynı malzeme tekrar eklendi, işlem yapılmadı.");
                return;
            }

            collectedIngredients.Add(other.gameObject);
            Destroy(other.gameObject); 

            Debug.Log($"🍲 Tenceredeki malzeme sayısı: {collectedIngredients.Count}/{recipeManager.selectedRecipe.ingredients.Count}");

            // ✅ Tüm malzemeler eklendiğinde pişirme işlemini başlat
            if (collectedIngredients.Count == recipeManager.selectedRecipe.ingredients.Count)
            {
                Debug.Log("🔥 Tüm malzemeler eklendi, yemek pişiyor: " + recipeManager.selectedRecipe.dishName);
                CookDish();
            }
        }
    }

    void CookDish()
    {
        if (dishSpawnPoint == null)
        {
            Debug.LogError("❌ Dish Spawn Point atanmadı!");
            return;
        }

        Debug.Log("🍳 Yemek pişiriliyor...");
        GameObject cookedDish = Instantiate(recipeManager.selectedRecipe.dishPrefab, dishSpawnPoint.position, Quaternion.identity);
        Debug.Log("✅ Yemek tamamlandı: " + cookedDish.name);

        Rigidbody rb = cookedDish.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.useGravity = true;
            rb.mass = 10f;
            rb.linearDamping = 5f;
            rb.angularDamping = 10f;
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }

        ResetPot();
    }

    void ResetPot()
    {
        Debug.Log("🧼 Tencere temizleniyor...");
        collectedIngredients.Clear();
    }
}

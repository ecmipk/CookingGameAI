using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Recipe
{
    public string dishName;
    public List<GameObject> ingredients;
    public GameObject dishPrefab;
}
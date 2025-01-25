using UnityEngine;

[CreateAssetMenu(fileName = "Attributes", menuName = "Scriptable Objects/Attributes")]
public class Attributes : ScriptableObject
{
    public string itemName; // Name of the food item
    public FoodType foodType; // Type of the food
    public float value; // Nutritional value for the specific type

    // Constructor
    public Attributes(string name, FoodType type, float value)
    {
        // this.itemName = name;
        this.itemName = default;
        this.foodType = type;
        this.value = value;
    }
}

public enum FoodType
{
    Veggie,
    Protein,
    Carbohydrate,
    Fat,
    Water
}
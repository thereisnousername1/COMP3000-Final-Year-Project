using Meta.XR.Editor.Tags;
using UnityEngine;
using UnityEngine.UI;

public class Trolley : MonoBehaviour
{
    public FoodManager foodManager;

    private void OnTriggerEnter(Collider other)
    {
        // Check if the object entering is a food item
        Attributes foodItem = other.GetComponent<FoodItemBehaviour>()?.foodItem;

        if (foodItem != null)
        {
            // Add food item to the food manager
            foodManager.CollectFood(foodItem);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Check if the object exiting is a food item
        Attributes foodItem = other.GetComponent<FoodItemBehaviour>()?.foodItem;

        if (foodItem != null)
        {
            // Remove food item from the food manager
            foodManager.RemoveFood(foodItem);
        }
    }
}
using Meta.XR.Editor.Tags;
using UnityEngine;
using UnityEngine.UI;

public class Trolley : MonoBehaviour
{
    public FoodManager foodManager;
    Attributes foodItem;

    private void OnTriggerEnter(Collider other)
    {
        // Check if the object entering is a food item
        // Attributes foodItem = other.GetComponent<FoodItemBehaviour>()?.foodItem;

        //Attributes foodItem = other.TryGetComponent<FoodItemBehaviour>()?.foodItem;

        //if(other.GetComponent<FoodItemBehaviour>() != null)

        foodItem = other.GetComponent<FoodItemBehaviour>()?.foodItem;

        if (foodItem != null)
        {
            // Add food item to the food manager
            foodManager.CollectFood(foodItem);
            // Destroy(other.gameObject);

            // try to fix the physics lag
            other.transform.SetParent(this.transform);
            // indeed it fixed, thanks chatgpt

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

            other.transform.SetParent(null);
        }
    }
}
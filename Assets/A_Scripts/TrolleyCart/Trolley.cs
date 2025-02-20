// using Meta.XR.Editor.Tags;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Trolley : MonoBehaviour
{
    public FoodManager foodManager;
    List<Attribute> foodItems;

    private void OnTriggerEnter(Collider other)
    {
        // buggy, it could treat player as children game object
        //if (!other.CompareTag("Floor"))
            // try to fix the physics lag
        //    other.transform.SetParent(this.transform);
            // indeed it fixed, thanks chatgpt

        // Check if the object entering is a food item
        // Attributes foodItem = other.GetComponent<FoodItemBehaviour>()?.foodItem;

        //bool value = other.TryGetComponent<FoodItemBehaviour>(out FoodItemBehaviour foodItem);

        //if(other.GetComponent<FoodItemBehaviour>() != null)

        //foodItem = other.GetComponent<FoodItemBehaviour>()?.foodItem;
        foodItems = other.GetComponent<FoodItemBehaviour>()?.foodItems;
        
        if (foodItems != null)
        {
            foreach (Attribute foodItem in foodItems)
            {
                // Add food item to the food manager
                foodManager.CollectFood(foodItem);
            }
            // Destroy(other.gameObject);

            /// moved to outside
            // try to fix the physics lag
            other.transform.SetParent(this.transform);
            // indeed it fixed, thanks chatgpt
            ///
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // other.transform.SetParent(null);

        // Check if the object exiting is a food item
        foodItems = other.GetComponent<FoodItemBehaviour>()?.foodItems;

        if (foodItems != null)
        {
            foreach (Attribute foodItem in foodItems)
            {
                // Remove food item from the food manager
                foodManager.RemoveFood(foodItem);
            }
            other.transform.SetParent(null);
        }
    }
}
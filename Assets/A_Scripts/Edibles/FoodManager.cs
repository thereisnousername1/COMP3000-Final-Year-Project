using UnityEngine;
using UnityEngine.UI;

public class FoodManager : MonoBehaviour
{
    public Slider veggieSlider;
    public Slider proteinSlider;
    public Slider carbonSlider;
    public Slider fatSlider;

    private float veggieAmount;
    private float proteinAmount;
    private float carbonAmount;
    private float fatAmount;

    // Method to add food item
    public void CollectFood(Attributes food)
    {
        switch (food.foodType)
        {
            case FoodType.Veggie:
                veggieAmount += food.value;
                veggieSlider.value = veggieAmount;
                break;
            case FoodType.Protein:
                proteinAmount += food.value;
                proteinSlider.value = proteinAmount;
                break;
            case FoodType.Carbohydrate:
                carbonAmount += food.value;
                carbonSlider.value = carbonAmount;
                break;
            case FoodType.Fat:
                fatAmount += food.value;
                fatSlider.value = fatAmount;
                break;
        }
    }

    // Method to remove food item
    public void RemoveFood(Attributes food)
    {
        switch (food.foodType)
        {
            case FoodType.Veggie:
                veggieAmount -= food.value;
                veggieSlider.value = Mathf.Max(veggieAmount, 0);
                break;
            case FoodType.Protein:
                proteinAmount -= food.value;
                proteinSlider.value = Mathf.Max(proteinAmount, 0); // Ensure value doesn't go negative
                break;
            case FoodType.Carbohydrate:
                carbonAmount -= food.value;
                carbonSlider.value = Mathf.Max(carbonAmount, 0);
                break;
            case FoodType.Fat:
                fatAmount -= food.value;
                fatSlider.value = Mathf.Max(fatAmount, 0);
                break;
        }
    }
}
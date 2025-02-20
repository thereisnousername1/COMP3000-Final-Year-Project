using UnityEngine;
using UnityEngine.UI;

// to be mapped to anything, usually I mapped it to the CartUI
public class FoodManager : MonoBehaviour
{
    public Slider veggieSlider;
    public Slider proteinSlider;
    public Slider carbonSlider;
    public Slider fatSlider;
    public Slider waterSlider;

    private float veggieAmount;
    private float proteinAmount;
    private float carbonAmount;
    private float fatAmount;
    private float waterAmount;

    // Method to add food item
    public void CollectFood(Attribute food)
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
            case FoodType.Water:
                waterAmount += food.value;
                waterSlider.value = waterAmount;
                break;
        }
    }

    // Method to remove food item
    public void RemoveFood(Attribute food)
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
            case FoodType.Water:
                waterAmount -= food.value;
                waterSlider.value = Mathf.Max(waterAmount, 0);
                break;
        }
    }
}
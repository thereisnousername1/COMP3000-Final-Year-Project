using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitChecking : MonoBehaviour
{
    // public FoodManager foodManager;

    public FadeScreen fadeScreen;

    // handled by scoring.cs instead
    // public float TotalScore;

    public static float VeggieScore;
    public static float CarbonScore;
    public static float ProteinScore;
    public static float FatScore;
    public static float WaterScore;

    List<Attributes> foodItems;

    public static int CurrentGameLevel;

    private void OnTriggerEnter(Collider other)
    {
        /* old logic
        // Check if the object entering is a trolley
        if (other.GetComponent<FoodManager>() != null)
        {
            Debug.Log("Detected trolley entered");
            // foodManager = other.GetComponent<FoodManager>();
            // foodManager.veggieSlider.value = 0;
            VeggieScore += other.GetComponent<FoodManager>().veggieSlider.value;
            CarbonScore += other.GetComponent<FoodManager>().carbonSlider.value;
            ProteinScore += other.GetComponent<FoodManager>().proteinSlider.value;
            FatScore += other.GetComponent<FoodManager>().fatSlider.value;
            WaterScore += other.GetComponent<FoodManager>().waterSlider.value;

            TotalScore += VeggieScore + CarbonScore + ProteinScore + FatScore + WaterScore;
        }
        */

        foodItems = other.GetComponent<FoodItemBehaviour>()?.foodItems;

        if (foodItems != null)
        {
            foreach (Attributes foodItem in foodItems)
            {
                switch (foodItem.foodType)
                {
                    case FoodType.Veggie:
                        VeggieScore += foodItem.value;
                        break;
                    case FoodType.Protein:
                        ProteinScore += foodItem.value;
                        break;
                    case FoodType.Carbohydrate:
                        CarbonScore += foodItem.value;
                        break;
                    case FoodType.Fat:
                        FatScore += foodItem.value;
                        break;
                    case FoodType.Water:
                        WaterScore += foodItem.value;
                        break;
                }

                //TotalScore += VeggieScore + CarbonScore + ProteinScore + FatScore + WaterScore;
            }
            // Destroy(other.gameObject);
        }

        if (other.gameObject.CompareTag("Player"))
        {
            // end current level and do score calculation, and then proceed to next level I guess
            // GoToCheckoutScene();

            CurrentGameLevel = SceneManager.GetActiveScene().buildIndex;
            //SceneTransitionManager.FadeScreenAnimation();
            //SceneManager.LoadScene("checkout");

            GoToCheckoutScene();
            Resources.UnloadUnusedAssets();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        /* old logic
        // Check if the object exiting is a trolley
        if (other.GetComponent<FoodManager>() != null)
        {
            Debug.Log("Detected trolley exited");

            VeggieScore -= other.GetComponent<FoodManager>().veggieSlider.value;
            CarbonScore -= other.GetComponent<FoodManager>().carbonSlider.value;
            ProteinScore -= other.GetComponent<FoodManager>().proteinSlider.value;
            FatScore -= other.GetComponent<FoodManager>().fatSlider.value;
            WaterScore -= other.GetComponent<FoodManager>().waterSlider.value;

            TotalScore -= (VeggieScore + CarbonScore + ProteinScore + FatScore + WaterScore);
        }
        */

        foodItems = other.GetComponent<FoodItemBehaviour>()?.foodItems;

        if (foodItems != null)
        {
            foreach (Attributes foodItem in foodItems)
            {
                switch (foodItem.foodType)
                {
                    case FoodType.Veggie:
                        VeggieScore -= foodItem.value;
                        break;
                    case FoodType.Protein:
                        ProteinScore -= foodItem.value;
                        break;
                    case FoodType.Carbohydrate:
                        CarbonScore -= foodItem.value;
                        break;
                    case FoodType.Fat:
                        FatScore -= foodItem.value;
                        break;
                    case FoodType.Water:
                        WaterScore -= foodItem.value;
                        break;
                }

                //TotalScore -= (VeggieScore + CarbonScore + ProteinScore + FatScore + WaterScore);
            }
            // Destroy(other.gameObject);
        }
    }

    void GoToCheckoutScene()
    {
        StartCoroutine(GoToCheckoutSceneRoutine());
    }

    private IEnumerator GoToCheckoutSceneRoutine()
    {
        fadeScreen.gameObject.SetActive(true);
        fadeScreen.FadeOut();
        yield return new WaitForSeconds(fadeScreen.fadeDuration);

        // set FadeScreen to inactive, to prevent blocking ray interactors and UI canvas
        fadeScreen.gameObject.SetActive(false);
        SceneManager.LoadScene("checkout");
    }
}

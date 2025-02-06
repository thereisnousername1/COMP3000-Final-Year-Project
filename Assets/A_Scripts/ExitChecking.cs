using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitChecking : MonoBehaviour
{
    // public FoodManager foodManager;

    public FadeScreen fadeScreen;

    public float TotalScore;

    public static float VeggieScore;
    public static float CarbonScore;
    public static float ProteinScore;
    public static float FatScore;
    public static float WaterScore;

    private void OnTriggerEnter(Collider other)
    {
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

        if (other.gameObject.CompareTag("Player"))
        {
            // end current level and do score calculation, and then proceed to next level I guess
            GoToCheckoutScene();
        }
    }

    private void OnTriggerExit(Collider other)
    {
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

        SceneManager.LoadScene("checkout");
    }
}

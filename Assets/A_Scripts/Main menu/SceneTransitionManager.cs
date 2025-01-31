using Meta.XR.MRUtilityKit;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransitionManager : MonoBehaviour
{
    public FadeScreen fadeScreen;
    public GameObject spawnPoint;
    private GameObject player;

    [SerializeField]
    private string targetScene;

    private void Start()
    {
        StartScene();
    }

    private void StartScene()
    {
        StartCoroutine(StartSceneRoutine());
    }

    private IEnumerator StartSceneRoutine()
    {
        player = GameObject.FindWithTag("Player");
        player.transform.position = spawnPoint.transform.position;
        player.transform.rotation = spawnPoint.transform.rotation;
        
        fadeScreen.gameObject.SetActive(true);
        fadeScreen.FadeIn();
        yield return new WaitForSeconds(fadeScreen.fadeDuration);

        // set FadeScreen to inactive, to prevent blocking ray interactors and UI canvas
        fadeScreen.gameObject.SetActive(false);
    }

    public void GoToScene()
    {
        StartCoroutine(GoToSceneRoutine());
    }

    private IEnumerator GoToSceneRoutine()
    {
        fadeScreen.gameObject.SetActive(true);
        fadeScreen.FadeOut();
        yield return new WaitForSeconds(fadeScreen.fadeDuration);

        SceneManager.LoadScene(targetScene);
    }

    /*  pause the game in the GameMenuManager
    public void PauseGame()
    {
        Time.timeScale = 0;
    }
    */

    public void ResumeGame()
    {
        Time.timeScale = 1;
    }

}

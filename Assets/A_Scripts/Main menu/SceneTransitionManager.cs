using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransitionManager : MonoBehaviour
{
    public FadeScreen fadeScreen;

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

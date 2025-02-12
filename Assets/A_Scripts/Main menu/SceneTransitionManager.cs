using Meta.XR.MRUtilityKit;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransitionManager : MonoBehaviour
{
    public FadeScreen fadeScreen;
    //public GameObject spawnPoint;
    //private GameObject player;

    // [SerializeField]
    // private string targetScene;

#region Start
    // execute every time when scene changed
    private void Start()
    {
        Debug.Log(SceneManager.GetSceneByBuildIndex(0));

        //if(fadeScreen == null)
        //    findPlayerFadeScreen();
        StartScene();
    }

    private void StartScene()
    {
        StartCoroutine(StartSceneRoutine());
    }

    private IEnumerator StartSceneRoutine()
    {
        //player = GameObject.FindWithTag("Player");
        //player.transform.position = spawnPoint.transform.position;
        //player.transform.rotation = spawnPoint.transform.rotation;

        fadeScreen.gameObject.SetActive(true);
        fadeScreen.FadeIn();
        yield return new WaitForSeconds(fadeScreen.fadeDuration);

        // set FadeScreen to inactive, to prevent blocking ray interactors and UI canvas
        fadeScreen.gameObject.SetActive(false);
    }
#endregion

    /// <summary>
    /// using string to find a scene
    /// </summary>

    /* okay I am ready to replace this whole thing
    public void GoToScene()
    {
        //StartCoroutine(GoToSceneRoutine());

        StartCoroutine(FadeScreenAnimation());
        SceneManager.LoadScene(targetScene);
    }

    /*
    private IEnumerator GoToSceneRoutine()
    {
        fadeScreen.gameObject.SetActive(true);
        fadeScreen.FadeOut();
        yield return new WaitForSeconds(fadeScreen.fadeDuration);

        // set FadeScreen to inactive, to prevent blocking ray interactors and UI canvas
        fadeScreen.gameObject.SetActive(false);

        SceneManager.LoadScene(targetScene);
    }
    */

#region Multiple scene
    public void GoToScene(string scene)
    {
        //StartCoroutine(GoToSceneRoutine(scene));
        StartCoroutine(FadeScreenAnimation());
        SceneManager.LoadScene(scene);

        // get current scene as debug.log for further development
        // Debug.Log("Scene number: " + SceneManager.sceneCount);
    }

    /*
    private IEnumerator GoToSceneRoutine(string scene)
    {
        fadeScreen.gameObject.SetActive(true);
        fadeScreen.FadeOut();
        yield return new WaitForSeconds(fadeScreen.fadeDuration);

        SceneManager.LoadScene(scene);
    }
    */
#endregion

#region Back to Main menu with score reset
    public void GoBack()
    {
        // restore data to prevent cheating
        ExitChecking.VeggieScore = 0;
        ExitChecking.CarbonScore = 0;
        ExitChecking.ProteinScore = 0;
        ExitChecking.FatScore = 0;
        ExitChecking.WaterScore = 0;

        //StartCoroutine(GoBackRoutine());
        StartCoroutine(FadeScreenAnimation());
        SceneManager.LoadScene("startpage");
    }

    /*
    private IEnumerator GoBackRoutine()
    {
        fadeScreen.gameObject.SetActive(true);
        fadeScreen.FadeOut();
        yield return new WaitForSeconds(fadeScreen.fadeDuration);

        SceneManager.LoadScene("startpage");
    }
    */

#endregion

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

    public IEnumerator FadeScreenAnimation()
    {
        fadeScreen.gameObject.SetActive(true);
        fadeScreen.FadeOut();
        yield return new WaitForSeconds(fadeScreen.fadeDuration);

        // set FadeScreen to inactive, to prevent blocking ray interactors and UI canvas
        fadeScreen.gameObject.SetActive(false);
    }

    /*
    public void findPlayerFadeScreen()
    {
        player = FindAnyObjectByType<GameObject>();
        if (player == gameObject.CompareTag("Player"))
        {
            fadeScreen = player.GetComponentInChildren<FadeScreen>();
        }
    }
    */
}

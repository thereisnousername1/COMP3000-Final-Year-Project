using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HoomanVariant1 : HoomanBehavior
{
    public FadeScreen fadeScreen;

    [SerializeField]
    private string targetScene;

    private void Update()
    {
        if (isKnockedBack)
        {
            StartCoroutine(GoToSceneRoutine());
        }
    }



private IEnumerator GoToSceneRoutine()
    {
        fadeScreen.gameObject.SetActive(true);
        fadeScreen.FadeOut();
        yield return new WaitForSeconds(fadeScreen.fadeDuration);

        SceneManager.LoadScene(targetScene);
    }
}

using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwap : MonoBehaviour
{
    [SerializeField]
    private string scene1;

    // for development stage
    public void Swap()
    {
        SceneManager.LoadScene(scene1);
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

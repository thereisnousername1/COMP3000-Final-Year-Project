using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwap : MonoBehaviour
{
    public string scene1;
    public void Swap()
    {
        SceneManager.LoadScene(scene1);
    }
}

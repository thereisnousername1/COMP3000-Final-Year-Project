using UnityEngine.InputSystem;
using UnityEngine;
using Unity.VisualScripting;

/// <summary>
///  This script is applied for XR Origin to perform a simple VR pause menu, NOT HAND MENU
/// </summary>
public class GameMenuManager : MonoBehaviour
{
    public Transform head;
    public float spawnDistance;
    public GameObject menu;
    public InputActionProperty showMenuButton;

    private Vector3 relativePosition;

    void Update()
    {
        // do only once (if an action was perform in a specific frame)
        if (showMenuButton.action.WasPerformedThisFrame())
        {
            menu.SetActive(!menu.activeSelf);

            // I assume it will also do only once??
            if (menu.activeSelf)
            {
                // pause game
                Time.timeScale = 0;

                menu.transform.position = head.position + new Vector3(head.forward.x, 0, head.forward.z).normalized * spawnDistance;
                relativePosition = new Vector3(menu.transform.position.x - head.position.x, 0, menu.transform.position.z - head.position.z);
            }
            else
            {
                // resume game
                Time.timeScale = 1;
            }

            menu.transform.position = head.position + relativePosition;

            menu.transform.LookAt(new Vector3(head.position.x, menu.transform.position.y, head.position.z));
            menu.transform.forward *= -1;
        }
    }
}
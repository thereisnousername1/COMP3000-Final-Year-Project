using UnityEngine.InputSystem;
using UnityEngine;

public class GameMenuManager : MonoBehaviour
{
    public Transform head;
    public float spawnDistance;
    public GameObject menu;
    public InputActionProperty showMenuButton;

    private Vector3 relativePosition;

    void Update()
    {
        if (showMenuButton.action.WasPerformedThisFrame())
        {
            menu.SetActive(!menu.activeSelf);

            if (menu.activeSelf)
            {
                menu.transform.position = head.position + new Vector3(head.forward.x, 0, head.forward.z).normalized * spawnDistance;
                relativePosition = new Vector3(menu.transform.position.x - head.position.x, 0, menu.transform.position.z - head.position.z);
            }
        }

        menu.transform.position = head.position + relativePosition;

        menu.transform.LookAt(new Vector3(head.position.x, menu.transform.position.y, head.position.z));
        menu.transform.forward *= -1;
    }
}
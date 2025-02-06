using UnityEngine.InputSystem;
using UnityEngine;
using Unity.VisualScripting;
using UnityEngine.UIElements;

/// <summary>
///  This script is applied for XR Origin to perform a simple VR pause menu, NOT HAND MENU
/// </summary>
public class GameMenuManager : MonoBehaviour
{
    public Transform head;
    public float spawnDistance;
    public GameObject menu;
    public InputActionProperty showMenuButton;

    public Transform RightHand;
    public GameObject UI;
    public InputActionProperty showUIButton;

    private Vector3 relativePosition;

    Vector3 horizontalForward;
    float angle;
    Vector3 menuHorizontalPosition;
    Vector3 menuPosition;

    public float cylinderRadius = 0.15f; // Radius of the wrist/cylinder
    public float offsetAngle = 0f; // Initial offset along the cylinder
    public float menuVerticalOffset = 0.1f; // Vertical offset relative to the hand

    void Update()
    {

#region Pause Menu behaviour
        // do only once (if an action was perform in a specific frame)
        if (showMenuButton.action.WasPerformedThisFrame())
        {
            menu.SetActive(!menu.activeSelf);

            // I assume it will also do only once??
            if (menu.activeSelf)
            {
                // pause game while button is clicked
                Time.timeScale = 0;

                menu.transform.position = head.position + new Vector3(head.forward.x, 0, head.forward.z).normalized * spawnDistance;

                // for when player moving it still draw at a desired draw distance
                relativePosition = new Vector3(menu.transform.position.x - head.position.x, 0, menu.transform.position.z - head.position.z);
            }
            else
            {
                // resume game
                Time.timeScale = 1;
            }

            // for when player moving it still draw at a desired draw distance
            menu.transform.position = head.position + relativePosition;

            menu.transform.LookAt(new Vector3(head.position.x, menu.transform.position.y, head.position.z));
            menu.transform.forward *= -1;
        }
#endregion

        if (showUIButton.action.WasPerformedThisFrame())
        {
            UI.SetActive(!UI.activeSelf);
        }

        // Project hand forward vector onto the horizontal plane
        horizontalForward = Vector3.ProjectOnPlane(RightHand.forward, Vector3.up).normalized;

        // Calculate the cylindrical position based on the offset angle
        angle = Mathf.Atan2(horizontalForward.z, horizontalForward.x) + offsetAngle;
        menuHorizontalPosition = new Vector3(
            Mathf.Cos(angle) * cylinderRadius,
            0,
            Mathf.Sin(angle) * cylinderRadius
        );

        menuPosition = RightHand.position + menuHorizontalPosition;
        menuPosition.y = RightHand.position.y + menuVerticalOffset;
        UI.transform.position = menuPosition;
        UI.transform.forward *= 1;
        // UI.transform.LookAt(new Vector3(UI.transform.position.x, head.position.y, UI.transform.position.z));

        UI.transform.rotation = Quaternion.LookRotation(head.position - UI.transform.position, Vector3.up);
    }
}
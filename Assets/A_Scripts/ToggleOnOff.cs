using UnityEngine;

public class ToggleOnOff : MonoBehaviour
{
    bool isOn;

    public void SwapState()
    {
        isOn = this.gameObject.activeSelf;
        this.gameObject.SetActive(!isOn);
    }
}

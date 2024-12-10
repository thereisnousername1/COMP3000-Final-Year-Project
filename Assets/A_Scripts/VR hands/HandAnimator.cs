using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// This script is applied for the hands models, to simulate real hand motion
/// </summary>
public class HandAnimator : MonoBehaviour
{
    public InputActionProperty pinchInput;
    public InputActionProperty gripInput;

    public Animator animator;

    // Update is called once per frame
    void Update()
    {
        float triggerValue = pinchInput.action.ReadValue<float>();
        float gripValue = gripInput.action.ReadValue<float>();

        animator.SetFloat("Trigger", triggerValue);
        animator.SetFloat("Grip", gripValue);
    }
}
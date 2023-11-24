using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    public enum InteractionType
    {
        Click,
        Hold,
        Minigame
    }
    public InteractionType interactionType;

    float holdTime;
    public abstract string GetDescription();
    public abstract void Interact();
    public abstract KeyCode GetKey();

    public abstract int GetRemainingAmount();

    public void IncreaseHoldTime() => holdTime += Time.deltaTime;
    public void ResetHoldTime() => holdTime = 0f;

    public float GetHoldTime() => holdTime;
}

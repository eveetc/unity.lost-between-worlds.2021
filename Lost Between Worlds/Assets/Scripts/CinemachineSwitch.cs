using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CinemachineSwitch : MonoBehaviour
{

    private Animator animator;
    private bool cameraAtNPC = false;
    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void SwitchState()
    {
        if (cameraAtNPC)
        {
            animator.Play("PortalCamera");
        }
        else
        {
            animator.Play("CharacterCamera");
        }
        cameraAtNPC = !cameraAtNPC;
    }
}

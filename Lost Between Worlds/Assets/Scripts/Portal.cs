using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Portal : Interactable
{
    public GameObject portalObject;

    public GameObject transportation;

    public GameObject camSwitch;
    private Animator animator;
    private bool cameraAtNPC = false;
    public SceneIndexes fromScene;
    public SceneIndexes toScene;

    private void Start()
    {
        animator = camSwitch.GetComponent<Animator>();
    }

    public override string GetDescription()
    {

        return "Press (t) to activate the portal";

    }


    public override void Interact()
    {
        PersistentGameManager manager = GameObject.Find("Game Manager").GetComponent<PersistentGameManager>();
        int origin = (int)fromScene;
        int dest = (int)toScene;
        manager.LoadGame(origin, dest);
    }

    public override int GetRemainingAmount()
    {
        return 1;
    }

    public override KeyCode GetKey()
    {
        return KeyCode.T;
    }


    IEnumerator TriggerTransportAnimations()
    {
        animator.Play("PortalCamera");
        yield return new WaitForSecondsRealtime(4);

        animator.Play("CharacterCamera");
        transportation.SetActive(false);

    }


}
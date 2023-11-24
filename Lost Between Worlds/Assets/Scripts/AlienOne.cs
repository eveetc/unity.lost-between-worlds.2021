using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AlienOne : Interactable
{
    public bool isOn;
    public GameObject questWindow;
    public override string GetDescription()
    {

        return "Press (q) to open quest";

    }


    public override void Interact()
    {
        isOn = !isOn;
        questWindow = GameObject.Find("UserQuestPanelParent").transform.GetChild(0).gameObject;
        questWindow.SetActive(isOn);
    }

    public override int GetRemainingAmount()
    {
        return 1;
    }

    public override KeyCode GetKey()
    {
        return KeyCode.Q;
    }

}

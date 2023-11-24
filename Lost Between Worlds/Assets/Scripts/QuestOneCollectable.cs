using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestOneCollectable : Interactable
{
    public bool isOn;

    public int remainingAmount;
    private Vector3 target;


    private void Start()
    {
        remainingAmount = 4;
        target = transform.position;
    }


    public override string GetDescription()
    {
        return "Hold (e) to mine";
    }

    public override void Interact()
    {
        isOn = !isOn;
        remainingAmount--;
        target = new Vector3(transform.position.x, transform.position.y - 0.1f * remainingAmount, transform.position.z);

    }

    private void Update()
    {
        if (target != transform.position)
        {
            transform.position = Vector3.Lerp(transform.position, target, Time.deltaTime);
        }

    }

    public override int GetRemainingAmount()
    {
        return remainingAmount;
    }

    public override KeyCode GetKey()
    {
        return KeyCode.E;
    }

}

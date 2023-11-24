using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Quest
{
    public bool isActive;
    public bool completed;
    public string title;
    public string description;
    public int expReward;
    public Item.ItemType reward;
    public QuestGoal goal;

    public void Complete()
    {
        isActive = false;
        Debug.Log(title + " completed");
        completed = true;
    }

    public void Reset(){
         completed = false;
    }
}

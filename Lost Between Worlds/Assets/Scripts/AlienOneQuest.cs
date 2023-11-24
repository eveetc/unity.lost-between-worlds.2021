using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AlienOneQuest : MonoBehaviour
{
    public Quest quest;

    public PlayerInteraction player;

    public GameObject questWindow;
    public TMPro.TextMeshProUGUI miniTaskPanelText;
    public TMPro.TextMeshProUGUI titleText;
    public TMPro.TextMeshProUGUI descriptionText;
    public Button acceptBtn;
    public Button rewardBtn;
    public Button declineBtn;

    public GameObject questOneItem;


    private void Start()
    {
        questWindow = GameObject.Find("UserQuestPanelParent").transform.GetChild(0).gameObject;
        miniTaskPanelText = GameObject.Find("UserTaskText").GetComponent<TMPro.TextMeshProUGUI>();
        titleText = GameObject.Find("QuestTitle").GetComponent<TMPro.TextMeshProUGUI>();
        descriptionText = GameObject.Find("QuestContent").GetComponent<TMPro.TextMeshProUGUI>();
        acceptBtn = GameObject.Find("QuestAccept").GetComponent<Button>();
        rewardBtn = GameObject.Find("QuestReward").GetComponent<Button>();
        declineBtn = GameObject.Find("QuestClose").GetComponent<Button>();


        titleText.text = quest.title;
        descriptionText.text = quest.description;
    }

    private void Update()
    {
        if (quest.completed)
        {
            miniTaskPanelText.text = "completed \ngo back";
            declineBtn.gameObject.SetActive(false);
            acceptBtn.gameObject.SetActive(false);
            rewardBtn.gameObject.SetActive(true);
            rewardBtn.onClick.AddListener(delegate { gotReward(); });
        }
    }

    private void gotReward()
    {
        questWindow.SetActive(false);
    }


    public void OpenQuestWindow()
    {
        questWindow.SetActive(true);
    }


    public void StartQuest()
    {
        questWindow.SetActive(false);
        quest.isActive = true;
        miniTaskPanelText.text = quest.title;
        player.quest = quest;
        questOneItem.gameObject.GetComponent<BoxCollider>().enabled = true;
    }

}

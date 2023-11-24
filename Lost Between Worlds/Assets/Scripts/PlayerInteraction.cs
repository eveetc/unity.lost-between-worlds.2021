using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInteraction : MonoBehaviour
{
    public float interactionDistance;

    public TMPro.TextMeshProUGUI interactionText;
    public GameObject interactionHoldObject;

    public UnityEngine.UI.Image interactionHoldProgress;

    bool successfulHit;

    bool activelyHoldingTask;

    public GameObject alienOne;

    public Quest quest;

    public int tritium;

    public TMPro.TextMeshProUGUI miniTaskPanelText;

    public GameObject canvas;

    public GameObject shovel;

    public GameObject fazer;
    private Animator anim;

    private GuiManager GUIManager;

    void Awake()
    {
        interactionText = GameObject.Find("UserInteractionText").GetComponent<TMPro.TextMeshProUGUI>();
        interactionHoldObject = GameObject.Find("UserHoldInteractionParent").transform.GetChild(0).gameObject;
        interactionHoldProgress = GameObject.Find("UserHoldInteractionParent").transform.GetChild(0).transform.GetChild(0).GetComponent<UnityEngine.UI.Image>();
        GUIManager = GameObject.Find("GUI Manager").GetComponent<GuiManager>();
        miniTaskPanelText = GameObject.Find("UserTaskText").GetComponent<TMPro.TextMeshProUGUI>();
        canvas = GameObject.Find("UserGUI");
    }
    void Start()
    {
        anim = gameObject.GetComponentInChildren<Animator>();
        tritium = 0;
        successfulHit = false;
        activelyHoldingTask = false;
    }

    public void ChangeSpawnPosition()
    {
        GameObject PlayerCamMikeParent = GameObject.Find("PlayerCamMikeParent");
        PlayerCamMikeParent.transform.position = new Vector3(0.0f, 0.0f, 160.0f);
        PlayerCamMikeParent.transform.rotation = Quaternion.Euler(0, 180, 0);
    }

    public void GoMining()
    {
        GUIManager.ManipulateExp(2);

        tritium++;
        GUIManager.AddToInventory(new Item { itemType = Item.ItemType.Tritium, amount = 1, damageOrHealing = 0 });


        if (quest.isActive)
        {
            quest.goal.ItemMined();
            if (quest.goal.IsReached())
            {
                GUIManager.ManipulateExp(quest.expReward);
                quest.Complete();
            }
        }
    }

    private void miningAnimations(bool activate)
    {
        if (anim.GetBool("holdWeapon") && activate)
        {
            GUIManager.ActivateFazer();
            activateWeapon(false);
        }
        bool isMining = anim.GetBool("isMining");
        shovel.SetActive(activate);
        anim.SetBool("isMining", activate);
    }

    public void activateWeapon(bool activate)
    {
        fazer.SetActive(activate);
        anim.SetBool("holdWeapon", activate);
    }

    public void GetReward()
    {
        GameObject.Find("AlienOne").GetComponent<BoxCollider>().enabled = false;
        print("Reward received");
        int randomDamage = Random.Range(0, 52);

        GUIManager.AddToInventory(new Item { itemType = Item.ItemType.Fazer, amount = 1, damageOrHealing = randomDamage });
        GUIManager.ManipulateExp(quest.expReward);
        tritium = tritium - 3; //TODO THIS IS HARDCODED
        GUIManager.RemoveFromInventory(new Item { itemType = Item.ItemType.Tritium, amount = 1 });
        GUIManager.RemoveFromInventory(new Item { itemType = Item.ItemType.Tritium, amount = 1 });
        GUIManager.RemoveFromInventory(new Item { itemType = Item.ItemType.Tritium, amount = 1 });
        quest.Reset();
        GUIManager.TriggerFinal();
        interactionText.text = "";
        alienOne.GetComponent<BoxCollider>().enabled = false;
    }

    void Update()
    {
        if (!successfulHit)
        {
            interactionText.text = "";
            interactionHoldObject.SetActive(false);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        ItemWorld itemWorld = other.GetComponent<ItemWorld>();

        if (itemWorld != null)
        {
            //handling collectable item
            GUIManager.AddToInventory(itemWorld.GetItem());
            itemWorld.DestroySelf();
        }
        else
        {
            //handling quest and interactions
            HandleCollisionEnter(other.gameObject);
        }
    }
    void OnCollisionEnter(Collision other)
    {
        HandleCollisionEnter(other.gameObject);
    }

    void HandleCollisionEnter(GameObject obj)
    {
        Interactable interactable = obj.GetComponent<Interactable>();
        if (interactable != null)
        {
            interactionText.text = interactable.GetDescription();
            successfulHit = true;
        }
    }
    private void OnTriggerStay(Collider other)
    {
        HandleCollisionStay(other.gameObject);
    }
    private void OnCollisionStay(Collision other)
    {
        HandleCollisionStay(other.gameObject);
    }
    void HandleCollisionStay(GameObject obj)
    {
        Interactable interactable = obj.GetComponent<Interactable>();
        if (interactable != null)
        {
            if (obj.CompareTag("Portal"))
            {
                if (Input.GetKeyDown(KeyCode.T))
                {
                    StartCoroutine(TriggerTemporaryUIHiding());

                    HandleInteraction(interactable);
                    var rotationVector = transform.rotation.eulerAngles;
                    rotationVector.y = 0; //TODO 90 degree
                    transform.rotation = Quaternion.Euler(rotationVector);
                    Debug.Log("portal trigger");
                }
            }
            else
            {
                HandleInteraction(interactable);
                interactable.gameObject.SetActive(interactable.GetRemainingAmount() > 0);
                if (activelyHoldingTask && interactable.GetRemainingAmount() == 0)
                {
                    activelyHoldingTask = false;
                    interactionHoldObject.SetActive(activelyHoldingTask);
                    miningAnimations(false);
                    interactionText.text = "";

                }
                else
                {
                    interactionHoldObject.SetActive(activelyHoldingTask);
                    interactionText.text = activelyHoldingTask ? "" : interactable.GetDescription();
                }
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        HandleCollisionExit();
    }
    private void OnCollisionExit(Collision other)
    {
        HandleCollisionExit();

    }

    void HandleCollisionExit()
    {
        successfulHit = false;
        activelyHoldingTask = false;
    }


    void HandleInteraction(Interactable interactable)
    {
        KeyCode key = interactable.GetKey();
        switch (interactable.interactionType)
        {
            case Interactable.InteractionType.Click:
                if (Input.GetKeyDown(key))
                {
                    interactable.Interact();
                }
                break;
            case Interactable.InteractionType.Hold:
                if (interactable.GetRemainingAmount() == 0)
                {
                    interactable.ResetHoldTime();
                }
                else if (Input.GetKey(key))
                {
                    miningAnimations(true);

                    activelyHoldingTask = true;

                    interactable.IncreaseHoldTime();
                    interactionHoldObject.SetActive(interactable.interactionType == Interactable.InteractionType.Hold);

                    if (interactable.GetHoldTime() > 1f)
                    {
                        interactable.Interact();
                        interactable.ResetHoldTime();
                        GoMining();
                    }
                }
                else
                {
                    activelyHoldingTask = false;
                    interactable.ResetHoldTime();
                }
                interactionHoldProgress.fillAmount = interactable.GetHoldTime();

                break;
            case Interactable.InteractionType.Minigame:
                //TODO
                break;
            default:
                throw new System.Exception("Unsupported type of interactable.");
        }

    }


    IEnumerator TriggerTemporaryUIHiding()
    {
        canvas.SetActive(false);
        yield return new WaitForSecondsRealtime(4);
        canvas.SetActive(true);
    }

}


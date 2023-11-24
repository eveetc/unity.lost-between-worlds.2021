using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GuiManager : MonoBehaviour
{

    public UnityEngine.UI.Image healthBar;
    public UnityEngine.UI.Image oxygenBar;

    public int health;
    public int oxygen;


    public bool helmetOn = true;

    public TMPro.TextMeshProUGUI helmetText;

    public UnityEngine.UI.Image expBar;
    public TMPro.TextMeshProUGUI expText;

    public TMPro.TextMeshProUGUI miniTaskPanelText;

    public int exp;


    [SerializeField] private UI_Inventory ui_Inventory;
    private Inventory inventory;
    private bool fazerInUse = false;

    public PlayerInteraction PlayerInteraction;

    void Start()
    {
        healthBar = GameObject.Find("UserHealthPanel").GetComponent<UnityEngine.UI.Image>();
        oxygenBar = GameObject.Find("UserOxygenPanel").GetComponent<UnityEngine.UI.Image>();
        helmetText = GameObject.Find("UserHelmetText").GetComponent<TMPro.TextMeshProUGUI>();
        expBar = GameObject.Find("UserExpPanel").GetComponent<UnityEngine.UI.Image>();
        expText = GameObject.Find("UserExpText").GetComponent<TMPro.TextMeshProUGUI>();
        ui_Inventory = GameObject.Find("UserQuickInventory").GetComponent<UI_Inventory>();
        miniTaskPanelText = GameObject.Find("UserTaskText").GetComponent<TMPro.TextMeshProUGUI>();

        PlayerInteraction = GameObject.Find("Player_Astronaut").GetComponent<PlayerInteraction>();

        health = 70;
        oxygen = 100;
        exp = 42;
        helmetOn = true;

        inventory = new Inventory(UseItem);
        ui_Inventory.SetInventory(inventory);


        InvokeRepeating("SetOxygenBar", 1.0f, 2f);

        /*   ItemWorld.SpawnItemWorld(new Vector3(15, 0.5f, 20), new Item { itemType = Item.ItemType.Medkit, amount = 1, damageOrHealing = 10 });
          ItemWorld.SpawnItemWorld(new Vector3(10, 0.5f, 24), new Item { itemType = Item.ItemType.Medkit, amount = 1, damageOrHealing = 10 });
          ItemWorld.SpawnItemWorld(new Vector3(-10, 0.5f, 15), new Item { itemType = Item.ItemType.Medkit, amount = 1, damageOrHealing = 10 });
          ItemWorld.SpawnItemWorld(new Vector3(-10, 0.5f, 5), new Item { itemType = Item.ItemType.Medkit, amount = 1, damageOrHealing = 10 });

          int randomWeaponSpawn = Random.Range(2, 10);
          for (int i = 0; i < randomWeaponSpawn; i++)
          {
              int randomX = Random.Range(-20, 20);
              int randomZ = Random.Range(-8, 20);
              int randomDamage = Random.Range(0, 42);
              ItemWorld.SpawnItemWorld(new Vector3(randomX, 0.5f, randomZ), new Item { itemType = Item.ItemType.Fazer, amount = 1, damageOrHealing = randomDamage });
          }
           */

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            HelmetInteraction();
        }

    }

    void FixedUpdate()
    {
        // exp / level calculations (WIP)
        float recentExp = expBar.fillAmount;
        expBar.fillAmount = Mathf.Lerp(recentExp, 0.10f * (exp % 20), Time.deltaTime);
        expText.text = "Level " + Mathf.Floor(exp / 20);

        //update health bar
        float recentHealth = healthBar.fillAmount;
        healthBar.fillAmount = Mathf.Lerp(recentHealth, 0.01f * health, Time.deltaTime);


        //update oxygen bar
        float recentOxygen = oxygenBar.fillAmount;
        oxygenBar.fillAmount = Mathf.Lerp(recentOxygen, 0.01f * oxygen, Time.deltaTime);
    }

    public void UpdatePlayer()
    {
        PlayerInteraction = GameObject.Find("Player_Astronaut").GetComponent<PlayerInteraction>();
    }

    void ReduceHealthBar()
    {
        health--;
        if (health == 0)
        {
            PersistentGameManager manager = GameObject.Find("Game Manager").GetComponent<PersistentGameManager>();
            int origin = (int)SceneManager.GetActiveScene().buildIndex;
            int dest = (int)SceneIndexes.GAMEOVER;
            manager.LoadGame(origin, dest);
        }
    }

    void SetOxygenBar()
    {
        if (helmetOn)
        {
            oxygen--;

            if (oxygen == 0)
            {
                InvokeRepeating("ReduceHealthBar", 1.0f, 0.5f);
            }
        }
    }


    public void AddToInventory(Item item)
    {
        print(item);
        print(inventory);
        inventory.AddItem(item);
    }

    public void RemoveFromInventory(Item item)
    {
        inventory.RemoveItem(item);
    }

    private void UseItem(Item item)
    {
        switch (item.itemType)
        {
            case Item.ItemType.Medkit:
                ManipulateHealth(100);
                RemoveFromInventory(item);
                break;
            case Item.ItemType.Tritium:
                //todo
                Debug.Log("nothing to do here");
                break;
            case Item.ItemType.OxyxgenCap:
                ManipulateHealth(100);
                RemoveFromInventory(item);
                break;
            case Item.ItemType.Fazer:
                ActivateFazer();
                PlayerInteraction.activateWeapon(fazerInUse);

                Debug.Log("Click on Weapon again to hide it");
                break;
        }
    }

    void HelmetInteraction()
    {
        helmetOn = !helmetOn;
        helmetText.text = helmetOn ? "Helmet on (h)" : "Helmet off (h)";
    }

    public void ManipulateExp(int amount)
    {
        exp = exp + amount;
    }

    public void ManipulateHealth(int amount)
    {
        health = 100;
    }

    public void ActivateFazer()
    {
        fazerInUse = !fazerInUse;
    }

    public void TriggerFinal()
    {
        miniTaskPanelText.text = "Go back to start\ntravel home";
    }
}

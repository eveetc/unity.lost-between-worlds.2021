using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Inventory : MonoBehaviour
{
    private Inventory inventory;
    private Transform itemSlotContainer;
    private Transform itemSlotTemplate;


    void Awake()
    {
        itemSlotContainer = transform.Find("UserItemSlotContainer");
        itemSlotTemplate = itemSlotContainer.Find("UserItemSlotTemplate");
    }
    public void SetInventory(Inventory inventory)
    {
        this.inventory = inventory;
        inventory.OnItemListChanged += Inventory_OnItemListChanged;

        RefreshInventoryItems();
    }

    private void Inventory_OnItemListChanged(object sender, System.EventArgs e)
    {
        RefreshInventoryItems();
    }
    public void RefreshInventoryItems()
    {
        print(itemSlotContainer);
        int prevAmountOfItems = inventory.GetItemList().Count;
        foreach (Transform child in itemSlotContainer)
        {
            if (child == itemSlotTemplate) continue;
            Destroy(child.gameObject);
        }
        int i = 0;
        int x = 0;
        int y = 0;
        float itemSlotCellSize = 50f;
        foreach (Item item in inventory.GetItemList())
        {
            RectTransform itemSlotRectTransform = Instantiate(itemSlotTemplate, itemSlotContainer).GetComponent<RectTransform>();
            itemSlotRectTransform.gameObject.SetActive(true);

            itemSlotRectTransform.GetComponent<Button>().onClick.AddListener(delegate { inventory.UseItem(item); });


            //animate newly added item  
            if (prevAmountOfItems - 1 == i)
            {
                Animation anim = itemSlotRectTransform.gameObject.GetComponent<Animation>();
                anim.Play("AddInventoryItem");
            }


            itemSlotRectTransform.anchoredPosition = new Vector2(x * itemSlotCellSize, y * itemSlotCellSize);
            Image image = itemSlotRectTransform.Find("Image").GetComponent<Image>();
            image.sprite = item.GetSprite();
            x++;
            if (x > 15)
            {
                x = 0;
                y++;
            }

            i++;
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory
{
    private List<Item> itemList;
    private Action<Item> useItemAction;

    public event EventHandler OnItemListChanged;


    public Inventory(Action<Item> useItemAction)
    {
        this.useItemAction = useItemAction;
        itemList = new List<Item>();

        //adds items to users inventory. Fazer item outcommented, but it can be used to test the goal of the game 
        //AddItem(new Item { itemType = Item.ItemType.Fazer, amount = 1, damageOrHealing = 10 });
        AddItem(new Item { itemType = Item.ItemType.Medkit, amount = 1, damageOrHealing = 10 });
        AddItem(new Item { itemType = Item.ItemType.OxyxgenCap, amount = 1, damageOrHealing = 10 });
    }

    public void AddItem(Item item)
    {
        itemList.Add(item);
        OnItemListChanged?.Invoke(this, EventArgs.Empty);
    }

    public List<Item> GetItemList()
    {
        return itemList;
    }

    public void RemoveItem(Item item)
    {
        for (int i = itemList.Count - 1; i >= 0; i--)
        {
            if (itemList[i].itemType == item.itemType && itemList[i].amount == item.amount)
            {
                itemList.RemoveAt(i);
                break;
            }
        }

        OnItemListChanged?.Invoke(this, EventArgs.Empty);
    }

    public void UseItem(Item item)
    {
        useItemAction(item);
    }

}

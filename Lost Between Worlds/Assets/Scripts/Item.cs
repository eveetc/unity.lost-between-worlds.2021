using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item
{
    public enum ItemType
    {
        Fazer,
        Medkit,
        OxyxgenCap,
        Tritium
    }

    public ItemType itemType;
    public int amount;

    public int damageOrHealing;

    public Sprite GetSprite()
    {
        switch (itemType)
        {
            default:
            case ItemType.Fazer:
                return ItemAssets.Instance.fazerSprite;
            case ItemType.Medkit:
                return ItemAssets.Instance.MedkitSprite;
            case ItemType.OxyxgenCap:
                return ItemAssets.Instance.oxygenCapSprite;
            case ItemType.Tritium:
                return ItemAssets.Instance.tritiumSprite;
        }
    }


}

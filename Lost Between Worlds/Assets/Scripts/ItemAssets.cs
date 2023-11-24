using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemAssets : MonoBehaviour
{
    public static ItemAssets Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }
    public Transform medItemWorld;
    public Transform fazerItemWorld;

    public Sprite fazerSprite;
    public Sprite tritiumSprite;
    public Sprite oxygenCapSprite;
    public Sprite MedkitSprite;
}

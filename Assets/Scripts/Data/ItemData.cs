using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ItemData : ScriptableObject
{
    [SerializeField] string id;

    [SerializeField] string itemName;

    [SerializeField] int price;

    [SerializeField] Sprite iconSprite;

    public string ID => id;
    public string Name => itemName;
    public int Price => price;
    public Sprite IconSprite => iconSprite;

}

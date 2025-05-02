using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ItemData : ScriptableObject
{
    [SerializeField] GameEnum.ItemType type;

    [SerializeField] string id;

    [SerializeField] string itemName;

    [SerializeField] int price;

    [SerializeField] Sprite iconSprite;


    public GameEnum.ItemType Type => type;
    public string ID => id;
    public string Name => itemName;
    public int Price => price;
    public Sprite IconSprite => iconSprite;

}

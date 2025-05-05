using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ItemData : ScriptableObject
{
    [SerializeField] GameEnum.ItemType type;

    [SerializeField] string itemName;

    [SerializeField] int price;

    [SerializeField] Sprite iconSprite;

    [SerializeField] StatData statData;


    public string ItemName => itemName;
    public int Price => price;
    public Sprite IconSprite => iconSprite;
    public StatData StatData => statData;
    public GameEnum.ItemType Type => type;

}

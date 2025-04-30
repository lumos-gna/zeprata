using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D.Animation;


[CreateAssetMenu(menuName = "ScriptableObjects/SpriteAssetItemData")]
public class SpriteAssetItemData : ScriptableObject
{
    [SerializeField] string id;

    [SerializeField] string itemName;
    
    [SerializeField] int price;

    [SerializeField] SpriteLibraryAsset spriteAsset;

    public string ID => id;
    public string Name => itemName;
    public int Price => price;
    public SpriteLibraryAsset SpriteAsset => spriteAsset;
}

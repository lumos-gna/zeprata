using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D.Animation;


[CreateAssetMenu(menuName = "ScriptableObjects/EquipmentItemData")]
public class EquipmentItemData : ItemData
{

    [SerializeField] SpriteLibraryAsset spriteAsset;

    public SpriteLibraryAsset SpriteAsset => spriteAsset;
}

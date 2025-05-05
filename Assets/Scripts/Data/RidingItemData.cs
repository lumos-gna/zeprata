using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "ScriptableObjects/RidingItemData")]
public class RidingItemData : EquipmentItemData
{
    [SerializeField] float mountHeight;
    public float MountHeight => mountHeight;
}

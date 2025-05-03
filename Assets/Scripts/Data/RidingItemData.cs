using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "ScriptableObjects/RidingItemData")]
public class RidingItemData : EquipmentItemData
{
    [SerializeField] Vector2 mountPoint; 
    public Vector2 MountPoint => mountPoint;
}

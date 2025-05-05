using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EquipmentController : MonoBehaviour
{
    public event UnityAction<bool, EquipmentItemData> OnToggleEquipEvent;

    public Dictionary<GameEnum.ItemType, EquipmentItemData> EquippedSlot => equippedSlot;


    Dictionary<GameEnum.ItemType, EquipmentItemData> equippedSlot = new();

    StatData targetStat;


    public void Init(StatData targetStat)
    {
        this.targetStat = targetStat;

        equippedSlot.Add(GameEnum.ItemType.Riding, null);
    }

    public void Equip(EquipmentItemData targetData)
    {
        GameEnum.ItemType type = targetData.Type;

        equippedSlot[type] = targetData;

        targetStat.ApplyTarget(true, targetData.StatData);

        OnToggleEquipEvent?.Invoke(true, targetData);
    }

    public void UnEquip(EquipmentItemData targetData)
    {
        equippedSlot[targetData.Type] = null;

        targetStat.ApplyTarget(false, targetData.StatData);

        OnToggleEquipEvent?.Invoke(false, targetData);
    }
}

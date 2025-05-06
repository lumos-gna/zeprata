using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EquipmentController : MonoBehaviour
{
    public event UnityAction<bool, EquipmentItemData> OnToggleEquipEvent;

    public Dictionary<GameEnum.ItemType, EquipmentItemData> EquippedDatas => equippedDatas;


    Dictionary<GameEnum.ItemType, EquipmentItemData> equippedDatas = new();

    StatData targetStat;


    public void Init(StatData targetStat)
    {
        this.targetStat = targetStat;

        equippedDatas.Add(GameEnum.ItemType.Riding, null);
    }


    public void Equip(EquipmentItemData targetData)
    {
        GameEnum.ItemType type = targetData.Type;

        if (equippedDatas[type] != null)
        {
            UnEquip(equippedDatas[type]);
        }

        equippedDatas[type] = targetData;

        targetStat.ApplyTarget(true, targetData.StatData);

        OnToggleEquipEvent?.Invoke(true, targetData);
    }

    public void UnEquip(EquipmentItemData targetData)
    {
        equippedDatas[targetData.Type] = null;

        targetStat.ApplyTarget(false, targetData.StatData);

        OnToggleEquipEvent?.Invoke(false, targetData);
    }
}

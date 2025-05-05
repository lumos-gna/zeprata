using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EquipmentController : MonoBehaviour
{
    public event UnityAction<bool, EquipmentItemData> OnToggleEquipEvent;

    public Dictionary<GameEnum.ItemType, EquipmentItemData> ItemDatas => itemDatas;


    Dictionary<GameEnum.ItemType, EquipmentItemData> itemDatas = new();

    StatData targetStat;


    public void Init(List<EquipmentItemData> equipmentItemDatas, StatData targetStat)
    {
        this.targetStat = targetStat;


        for (int i = 0; i < equipmentItemDatas.Count; i++) 
        {
            var targetData = equipmentItemDatas[i];

            itemDatas[targetData.Type] = targetData;
        }
    }


    public void ToggleEquip(EquipmentItemData itemData)
    {
        GameEnum.ItemType type = itemData.Type;

        itemDatas[type] = itemData;

        bool isEquip = itemData != null;

        targetStat.ApplyTarget(isEquip, itemData.StatData);

        OnToggleEquipEvent?.Invoke(isEquip, itemData);
    }
}

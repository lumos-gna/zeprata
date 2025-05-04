using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EquipmentController : MonoBehaviour
{
    public Dictionary<GameEnum.ItemType, EquipmentItemData> ItemDatas => itemDatas;

    public event UnityAction<EquipmentItemData> OnEquippedItem;

    public event UnityAction<EquipmentItemData> OnUnEquippedItem;


    Dictionary<GameEnum.ItemType, EquipmentItemData> itemDatas;


    private void Awake()
    {
        itemDatas = new();
    }


    public void Equip(EquipmentItemData itemData)
    {
        GameEnum.ItemType type = itemData.Type;

        if(itemDatas.ContainsKey(type)) 
        {
            itemDatas[type] = itemData;
        }
        else
        {
            itemDatas.Add(type, itemData);
        }

        OnEquippedItem?.Invoke(itemData);
    }

    public void UnEquip(EquipmentItemData itemData)
    {
        GameEnum.ItemType type = itemData.Type;

        if (itemDatas.ContainsKey(type))
        {
            itemDatas.Remove(type);
        }

        OnUnEquippedItem?.Invoke(itemData); 
    }
   
}

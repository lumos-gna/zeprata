using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EquipmentManager : Singleton<EquipmentManager>
{
    List<ItemData> equippedItemDatas;

    public event UnityAction<ItemData> OnEquipItem;
    public event UnityAction<ItemData> OnUnEquipItem;

    protected override void Awake()
    {
        base.Awake();
    }

    public void EquipItem(ItemData itemData)
    {
        var equippedItemData = equippedItemDatas.Find(item => item.Type == itemData.Type);

        if(equippedItemData != null) 
        {
            equippedItemData = itemData;
        }
        else
        {
            equippedItemDatas.Add(itemData);
        }
    }

    public void UnEquipItem(ItemData itemData)
    {
        equippedItemDatas.Remove(itemData);



    }


}

using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class InventoryManager : Singleton<InventoryManager>
{
    List<InventoryItemData> inventoryItemDatas;


    public event UnityAction<ItemData> OnEquippedItemData;
    public event UnityAction<ItemData> OnUnEquippedItemData;

    protected override void Awake()
    {
        base.Awake();

        inventoryItemDatas = DataManager.Instance.InventoryItemDatas;
    }


    public bool TryGetEquipItemData<T>(out T itemData) where T : ItemData
    {
        var targetTypeItem = inventoryItemDatas.Find(target => target is T);

        if(targetTypeItem != null )
        {
            if(targetTypeItem.IsEquip)
            {
                itemData = (T)targetTypeItem.ItemData;

                return true;
            }
        }

        itemData = null;
        return false;
    }



    public void EquipItem<T>(T itemData) where T : ItemData
    {
        var targetData = inventoryItemDatas.Find(target => target.ItemData == itemData);


        if (targetData != null) 
        {
            if (TryGetEquipItemData(out T equipItemData))
            {
                UnEquipItem(equipItemData);
            }

            targetData.IsEquip = true;

            OnEquippedItemData?.Invoke(itemData);
        }
    }

    public void UnEquipItem(ItemData itemData)
    {
        var targetData = inventoryItemDatas.Find(target => target.ItemData == itemData);

        if (targetData != null)
        {
            if(targetData.IsEquip) 
            {
                targetData.IsEquip = false;
            }

            OnUnEquippedItemData?.Invoke(itemData);
        }
    }


    public void AddItem(ItemData itemData, int count)
    {
        var targetData = inventoryItemDatas.Find(target => target.ItemData == itemData);

        if(targetData == null) 
        {
            var newItemData = new InventoryItemData()
            {
                ItemData = itemData,
                Count = count
            };

            inventoryItemDatas.Add(newItemData);
        }
        else
        {
            targetData.Count += count;

        }
    }

    public void RemoveItem(ItemData itemData, int count)
    {
        var targetData = inventoryItemDatas.Find(target => target.ItemData == itemData);

        if (targetData == null)
        {
            Debug.Log("invalid ItemData");
            return;
        }
        else
        {
            targetData.Count -= count;

            if(targetData.Count == 0) 
            {
                inventoryItemDatas.Remove(targetData);
            }
        }
    }
}

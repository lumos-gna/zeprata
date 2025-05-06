using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreController : MonoBehaviour
{
    public Dictionary<GameEnum.ItemType, List<StoreItemData>> StoreItemDatasByType => storeItemDatasByType;


    [SerializeField] ItemDataTable itemDataTable;


    Dictionary<GameEnum.ItemType, List<StoreItemData>> storeItemDatasByType;


    PlayerData playerData;


    public void Init(PlayerData playerData)
    {
        this.playerData = playerData;

        SetStoreItemDatasTypeDict(GameManager.Instance.StoreItemDatas);
    }

    void SetStoreItemDatasTypeDict(List<StoreItemData> storeItemDatas)
    {
        storeItemDatasByType = new();

        for (int i = 0; i < storeItemDatas.Count; i++)
        {
            var targetData = storeItemDatas[i];

            var dataType = targetData.ItemData.Type;

            if (!storeItemDatasByType.ContainsKey(dataType))
            {
                storeItemDatasByType.Add(dataType, new());
            }

            storeItemDatasByType[dataType].Add(targetData);
        }
    }

    public bool TryPurchaseItem(StoreItemData targetItem)
    {
        int itemPrice = targetItem.ItemData.Price;

        if (itemPrice <= playerData.gold)
        {
            targetItem.IsPurchased = true;

            playerData.gold -= itemPrice;


            var saveManager = SaveManager.Instance;

            var targetData = saveManager.SaveData.storeItems.Find(item => item.itemName == targetItem.ItemData.ItemName);

            saveManager.SaveData.playerGold = playerData.gold;

            if (targetData != null)
            {
                targetData.itemName = targetItem.ItemData.ItemName;
                targetData.isPurchased = true;
            }
            else
            {
                saveManager.SaveData.storeItems.Add(
                    new()
                    {
                        itemName = targetItem.ItemData.ItemName,
                        isPurchased = true
                    });
            }

            saveManager.Save();

            return true;
        }

        return false;
    }
}

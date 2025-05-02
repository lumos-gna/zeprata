using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopManager : Singleton<ShopManager>
{
    PlayerData playerData;
    InventoryManager inventoryManager;


    protected override void Awake()
    {
        base.Awake();

        var dataManager = DataManager.Instance;

        playerData = dataManager.PlayerData;

        inventoryManager = InventoryManager.Instance;
    }



    public bool TryBuyItem(ShopItemData shopItemData)
    {
        if(playerData.Gold >= shopItemData.ItemData.Price)
        {
            inventoryManager.AddItem(shopItemData.ItemData, 1);

            shopItemData.Count--;

            return true;
        }

        return false;
    }
}

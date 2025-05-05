using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreController : MonoBehaviour
{
    [SerializeField] ItemDataTable itemDataTable;

    PlayerData playerData;

    public void Init(PlayerData playerData)
    {
        this.playerData = playerData;
    }

    public bool TryPurchaseItem(StoreItemData targetItem)
    {
        var price = itemDataTable.TryGetItemData(targetItem.itemName, out ItemData itemData) ?
        itemData.Price :
        0;

        if (price <= playerData.gold)
        {
            targetItem.isPurchased = true;

            playerData.gold -= price;

            return true;
        }

        return false;
    }
}

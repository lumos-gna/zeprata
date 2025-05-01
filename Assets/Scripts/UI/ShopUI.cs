using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopUI : MonoBehaviour
{

    List<ShopItemData> shopItemDatas;

    DataManager dataManager;

    [SerializeField] TextMeshProUGUI playerGoldText;


    [SerializeField] ShopSpirteAssetSlot spriteAssetSlotPrefab;
    [SerializeField] Transform spriteAssetSlotParent;


    private void Start()
    {
        dataManager = DataManager.Instance;


        shopItemDatas = dataManager.ShopItemDatas;


        foreach (var item in shopItemDatas)
        {
            Instantiate(spriteAssetSlotPrefab, spriteAssetSlotParent).Init(item);
        }
    }

    private void Update()
    {
        playerGoldText.text = dataManager.PlayerData.Gold.ToString() + " G";
    }

    public void TryBuyItem(ShopItemData shopItemData)
    {
        var itemData = shopItemData.ItemData;

        if(itemData.Price <= dataManager.PlayerData.Gold)
        {
        }
    }
}

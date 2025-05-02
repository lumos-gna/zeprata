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


    [SerializeField] ShopItemSlot spriteAssetSlotPrefab;
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
}

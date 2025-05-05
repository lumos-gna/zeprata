using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StoreUI : MonoBehaviour, IPopupUI
{
    [SerializeField] ItemDataTable itemDataTable;
    [SerializeField] StoreController storeController;

    [Space(20f)]
    [SerializeField] StoreUISlot slotPreafb;

    [Space(20f)]
    [SerializeField] GridLayoutGroup layoutGroup;

    [Space(20f)]
    [SerializeField] TextMeshProUGUI titleText;

    [Space(20f)]
    [SerializeField] DynamicTextBox playerGoldInfo;
    [SerializeField] DynamicTextBox priceInfo;
 
    [Space(20f)]
    [SerializeField] TextMeshProUGUI equipButtonText;


    [Space(20f)]
    [SerializeField] Button applyButton;
    [SerializeField] Button buyButton;
    [SerializeField] Button closeButton;


    StoreUISlot previousSlot;
    StoreUISlot currentSlot;
    List<StoreUISlot> storeUISlots = new();

    Dictionary<GameEnum.ItemType, string> titleTextDict;
    Dictionary<bool, Button> stateButtonDict;

    DataManager dataManager;
    PlayerData playerData;
    EquipmentController equipmentController;


    Dictionary<GameEnum.ItemType, List<StoreItemData>> typeStoreItemDatas;


    public void Enable()
    {
        gameObject.SetActive(true);
    }

    public void Disable()
    {
        gameObject.SetActive(false);
    }


    public void Init(TownUIController townUIController, Player player)
    {
        playerData = player.Data;

        equipmentController = player.EquipmentController;

        storeController.Init(playerData);


        dataManager = DataManager.Instance;


        closeButton.onClick.AddListener(townUIController.DisablePopup);


        InitTypeStoreItemDatas(dataManager.StoreItemDatas);



        layoutGroup.cellSize = slotPreafb.GetComponent<RectTransform>().sizeDelta;

      
        titleTextDict = new()
        {
            { GameEnum.ItemType.Riding, "Riding"}
        };

        stateButtonDict = new()
        {
            { true, applyButton },
            { false, buyButton },
        };



        buyButton.onClick.AddListener(OnTryPurchase);
        applyButton.onClick.AddListener(OnToggleEquip);
    }


    public void InitUISate(GameEnum.ItemType targetType)
    {
        titleText.text = titleTextDict[targetType];

        equipButtonText.text = GetApplyText(currentSlot.StoreItemData.isPurchased, currentSlot.StoreItemData);

        playerGoldInfo.UpdateText(playerData.gold.ToString());

        InitSlots(typeStoreItemDatas[targetType]);
    }


    void InitTypeStoreItemDatas(List<StoreItemData> storeItemDatas)
    {
        typeStoreItemDatas = new();

        for (int i = 0; i < storeItemDatas.Count; i++)
        {
            var targetData = storeItemDatas[i];

            if (!typeStoreItemDatas.ContainsKey(targetData.type))
            {
                typeStoreItemDatas.Add(targetData.type, new());
            }

            typeStoreItemDatas[targetData.type].Add(targetData);
        }
    }



    void InitSlots(List<StoreItemData> typeStoreItemList)
    {
        for (int i = 0; i < typeStoreItemList.Count; i++)
        {
            if (storeUISlots.Count < typeStoreItemList.Count)
            {
                var createSlot = Instantiate(slotPreafb, layoutGroup.transform);

                createSlot.OnCreated(SelectSlot);

                storeUISlots.Add(createSlot);
            }
        }


        for (int i = 0; i < storeUISlots.Count; i++)
        {
            if (i < typeStoreItemList.Count)
            {
                storeUISlots[i].InitToItemData(typeStoreItemList[i], itemDataTable);
            }
            else
            {
                storeUISlots[i].gameObject.SetActive(false);
            }
        }
    }


    void SelectSlot(StoreUISlot targetSlot)
    {
        previousSlot = currentSlot;

        if(previousSlot != null) 
        {
            previousSlot.SelectedImage.gameObject.SetActive(false);
        }


        currentSlot = targetSlot;

        currentSlot.SelectedImage.gameObject.SetActive(true);


        var slotItem = currentSlot.StoreItemData;

        var price = itemDataTable.TryGetItemData(slotItem.itemName, out ItemData data) ? 
            data.Price :
            0;

        priceInfo.UpdateText(price.ToString());


        foreach (var item in stateButtonDict)
        {
            item.Value.gameObject.SetActive(item.Key == slotItem.isPurchased);
        }
    }



    void OnTryPurchase()
    {
        if (storeController.TryPurchaseItem(currentSlot.StoreItemData))
        {
            currentSlot.LockCoverImage.enabled = false;

            playerGoldInfo.UpdateText(playerData.gold.ToString());
        }
    }


    void OnToggleEquip()
    {
        EquipmentItemData targetData = null;


        if(itemDataTable.TryGetItemData(currentSlot.StoreItemData.itemName, out ItemData itemData))
        {
            if(itemData is EquipmentItemData equipmentData)
            {
                targetData = equipmentData;

            }
        }

        equipmentController.ToggleEquip(targetData);
    }


    string GetApplyText(bool isEquip, StoreItemData itemData)
    {
        switch (itemData.type)
        {
            case GameEnum.ItemType.Riding:
                return isEquip ? "Mount" : "Dismount";

            default: return "";
        }
    }

    
}

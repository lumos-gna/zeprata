using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StoreUI : MonoBehaviour, IPopupUI
{
    [SerializeField] StoreController storeController;

    [Space(20f)]
    [SerializeField] StoreUISlot slotPreafb;
    [SerializeField] GridLayoutGroup slotsLayoutGroup;

    [Space(10f)]
    [SerializeField] Button applyButton;
    [SerializeField] Button purchaseButton;
    [SerializeField] Button closeButton;

    [Space(10f)]
    [SerializeField] TextMeshProUGUI titleText;
    [SerializeField] TextMeshProUGUI applyButtonText;

    [Space(10f)]
    [SerializeField] DynamicTextBox playerGoldInfo;
    [SerializeField] DynamicTextBox priceInfo;


    PlayerData playerData;
    EquipmentController equipmentController;

    StoreUISlot previousSlot;
    StoreUISlot currentSlot;
    List<StoreUISlot> storeUISlots = new();



    public void Enable() => gameObject.SetActive(true);
    public void Disable() => gameObject.SetActive(false);


    public void Init(TownUIController townUIController, Player player)
    {
        playerData = player.Data;
        equipmentController = player.EquipmentController;

        storeController.Init(playerData);


        purchaseButton.onClick.AddListener(OnPurchase);

        applyButton.onClick.AddListener(OnApply);

        closeButton.onClick.AddListener(townUIController.DisablePopup);


        slotsLayoutGroup.cellSize = slotPreafb.GetComponent<RectTransform>().sizeDelta;
    }


    public void SetUIState(GameEnum.ItemType targetType)
    {
        var targetStoreItemList = storeController.StoreItemDatasByType[targetType];

        CreateSlots(targetStoreItemList);


        for (int i = 0; i < storeUISlots.Count; i++)
        {
            storeUISlots[i].SetSlot(targetStoreItemList[i]);

            if (i == 0)
            {
                SelectSlot(storeUISlots[i]);
            }


            var equipSlot = equipmentController.EquippedSlot[targetType];

            if(equipSlot != null)
            {
                if (equipSlot == storeUISlots[i].StoreItemData.ItemData)
                {
                    SelectSlot(storeUISlots[i]);
                }
            }
        }


        SetTittleText(targetType);

        SetApplyText(currentSlot.StoreItemData);

        playerGoldInfo.UpdateText(playerData.gold.ToString());
    }


    void CreateSlots(List<StoreItemData> typeStoreItemList)
    {
        for (int i = 0; i < typeStoreItemList.Count; i++)
        {
            if (storeUISlots.Count < typeStoreItemList.Count)
            {
                var createSlot = Instantiate(slotPreafb, slotsLayoutGroup.transform);

                createSlot.Init(SelectSlot);

                storeUISlots.Add(createSlot);
            }
            else if(storeUISlots.Count > typeStoreItemList.Count)
            {
                var targetSlot = storeUISlots[storeUISlots.Count - 1];

                targetSlot.gameObject.SetActive(false);

                storeUISlots.Remove(targetSlot);
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


        SetButtonState(slotItem.IsPurchased);

        SetApplyText(slotItem);

        priceInfo.UpdateText(slotItem.ItemData.Price.ToString());
    }


    void SetApplyText(StoreItemData storeItemData)
    {
        string text;

        var itemData = storeItemData.ItemData;

        switch (itemData.Type)
        {
            case GameEnum.ItemType.Riding: 
                text = equipmentController.EquippedSlot[itemData.Type] == itemData ?  "Dismount" : "Mount";
                break;

            default: text = ""; 
                break;
        }

        applyButtonText.text = text;
    }

    void SetTittleText(GameEnum.ItemType type)
    {
        string text;

        switch (type)
        {
            case GameEnum.ItemType.Riding: text = "Riding"; 
                break;
            default: text =  "";
                break;
        }

        titleText.text = text;
    }

    void SetButtonState(bool isPurchased)
    {
        applyButton.gameObject.SetActive(isPurchased);
        purchaseButton.gameObject.SetActive(!isPurchased);
    }

    void OnPurchase()
    {
        if (storeController.TryPurchaseItem(currentSlot.StoreItemData))
        {
            currentSlot.LockCoverImage.enabled = false;

            playerGoldInfo.UpdateText(playerData.gold.ToString());

            SetButtonState(true);

            SetApplyText(currentSlot.StoreItemData);
        }
    }

    void OnApply()
    {
        if (currentSlot.StoreItemData.ItemData is EquipmentItemData targetData)
        {
            var targetSlot = equipmentController.EquippedSlot[targetData.Type];

            if (targetSlot == currentSlot.StoreItemData.ItemData)
            {
                equipmentController.UnEquip(targetSlot);
            }
            else
            {
                equipmentController.Equip(targetData);
            }

            SetApplyText(currentSlot.StoreItemData);
        }
    }
}

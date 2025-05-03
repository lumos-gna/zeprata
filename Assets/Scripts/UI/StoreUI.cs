using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StoreUI : MonoBehaviour
{
    

    [Space(20f)]
    [SerializeField] StoreUISlot slotPreafb;

    [SerializeField] Transform slotsParent;

    [Space(20f)]
    [SerializeField] GridLayoutGroup layoutGroup;

    [Space(20f)]
    [SerializeField] TextMeshProUGUI titleText;

    [Space(20f)]
    [SerializeField] TextMeshProUGUI goldText;

    [SerializeField] RectTransform goldTextParent;

    [Space(20f)]
    [SerializeField] TextMeshProUGUI priceText;

    [SerializeField] RectTransform priceTextParent;

    [Space(20f)]
    [SerializeField] TextMeshProUGUI equipButtonText;


    [Space(20f)]
    [SerializeField] Button equipButton;

    [SerializeField] Button buyButton;

    [SerializeField] Button closeButton;


    StoreUISlot previousSlot;
    StoreUISlot currentSlot;
    List<StoreUISlot> storeUISlots = new();

    Dictionary<GameEnum.ItemType, string> titleTextDict;
    Dictionary<GameEnum.ItemType, string> equipTextDict;
    Dictionary<bool, Button> stateButtonDict;

    DataManager dataManager;
    PlayerData playerData;

    Vector2 goldInfoDefalutSize;
    Vector2 priceInfoDefalutSize;


    public void Init()
    {
        dataManager = DataManager.Instance;
        playerData = dataManager.PlayerData;

        layoutGroup.cellSize = slotPreafb.GetComponent<RectTransform>().sizeDelta;

        equipTextDict = new()
        {
            { GameEnum.ItemType.Riding, "Ride"}
        };

        titleTextDict = new()
        {
            { GameEnum.ItemType.Riding, "Riding"}
        };

        stateButtonDict = new()
        {
            { true, equipButton },
            { false, buyButton },
        };

        goldInfoDefalutSize = goldTextParent.sizeDelta;
        priceInfoDefalutSize = priceTextParent.sizeDelta;

        buyButton.onClick.AddListener(BuyItem);
    }


    public void InitToSlots(GameEnum.ItemType itemType)
    {
        titleText.text = titleTextDict[itemType];
        equipButtonText.text = equipTextDict[itemType];

        InitNumberText(goldText, goldTextParent, goldInfoDefalutSize, playerData.gold);


        var typeCustomizeItemList = GetTypeItemList(itemType, dataManager.StoreItemDatas);

        InitSlots(typeCustomizeItemList);
    }



    List<StoreItemData> GetTypeItemList(GameEnum.ItemType itemType, List<StoreItemData> allItemList)
    {
        var tpyeItmeList = new List<StoreItemData>();

        for (int i = 0; i < allItemList.Count; i++)
        {
            if (dataManager.ItemDataDict[allItemList[i].itemName].Type == itemType)
            {
                tpyeItmeList.Add(allItemList[i]);
            }
        }

        return tpyeItmeList;
    }

    void InitSlots(List<StoreItemData> typeStoreItemList)
    {
        for (int i = 0; i < typeStoreItemList.Count; i++)
        {
            if (storeUISlots.Count < typeStoreItemList.Count)
            {
                var createSlot = Instantiate(slotPreafb, slotsParent);

                createSlot.OnCreated(SelectSlot);

                storeUISlots.Add(createSlot);
            }
        }


        for (int i = 0; i < storeUISlots.Count; i++)
        {
            if (i < typeStoreItemList.Count)
            {
                storeUISlots[i].InitToItemData(typeStoreItemList[i]);
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


        var storeItem = currentSlot.StoreItemData;

        InitNumberText(
            priceText, 
            priceTextParent, 
            priceInfoDefalutSize, 
            dataManager.ItemDataDict[storeItem.itemName].Price);


        foreach (var item in stateButtonDict)
        {
            item.Value.gameObject.SetActive(item.Key == storeItem.isPurchased);
        }
    }

    void BuyItem()
    {
        var targetStoreItemData = currentSlot.StoreItemData;

        var itemData = dataManager.ItemDataDict[targetStoreItemData.itemName];


        if(itemData.Price <= playerData.gold)
        {
            currentSlot.LockCoverImage.enabled = false;

            targetStoreItemData.isPurchased = true;

            playerData.gold -= itemData.Price;


            InitNumberText(goldText, goldTextParent, goldInfoDefalutSize, playerData.gold);

            foreach (var item in stateButtonDict)
            {
                item.Value.gameObject.SetActive(item.Key == currentSlot.StoreItemData.isPurchased);
            }
        }
    }

    void EquipItem()
    {

    }


    void InitNumberText(TextMeshProUGUI targetText, RectTransform textParent, Vector2 defalutSize, int number)
    {
        targetText.text = number.ToString();

        int placeCount = 0;

        while(number > 0) 
        {
            number /= 10;

            placeCount++;

        }

        defalutSize.x += targetText.fontSize / 2 * placeCount;

        textParent.sizeDelta = defalutSize;

    }



 
}

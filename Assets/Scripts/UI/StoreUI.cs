using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StoreUI : MonoBehaviour
{
    StoreUISlot previousSlot;
    StoreUISlot currentSlot;
    List<StoreUISlot> storeUISlots = new();

    Dictionary<GameEnum.ItemType, string> titleTextDict;
    Dictionary<GameEnum.ItemType, string> equipTextDict;
    Dictionary<bool, Button> stateButtonDict;

    Vector2 goldInfoDefalutSize;
    Vector2 priceInfoDefalutSize;

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


    [Space(20f)]
    [SerializeField] List<StoreItemData> tesStoreItemList = new();

    [SerializeField] List<ItemData> testItemDataList = new();



    private void Awake()
    {
        layoutGroup.cellSize = slotPreafb.GetComponent<RectTransform>().sizeDelta;

        equipTextDict = new()
        {
            { GameEnum.ItemType.Character, "Equip"},
            { GameEnum.ItemType.Riding, "Ride"}
        };

        titleTextDict = new()
        {
            { GameEnum.ItemType.Character, "Character"},
            { GameEnum.ItemType.Riding, "Riding"}
        };

        stateButtonDict = new()
        {
            { true, equipButton },
            { false, buyButton },
        };

        goldInfoDefalutSize = goldTextParent.sizeDelta;
        priceInfoDefalutSize = priceTextParent.sizeDelta;
    }

    private void Start()
    {
        for (int i = 0; i < testItemDataList.Count; i++)
        {
            tesStoreItemList.Add(
                new()
                {
                    ItemData = testItemDataList[i],
                    IsPurchased = false
                }
            );
        }

        Init(GameEnum.ItemType.Character, tesStoreItemList);

        SelectSlot(storeUISlots[0]);

        buyButton.onClick.AddListener(BuyItem);
    }

    List<StoreItemData> GetTypeItemList(GameEnum.ItemType itemType, List<StoreItemData> allItemList)
    {
        var tpyeItmeList = new List<StoreItemData>();

        for (int i = 0; i < allItemList.Count; i++)
        {
            if (allItemList[i].ItemData.Type == itemType)
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


        InitNumberText(priceText, priceTextParent, priceInfoDefalutSize, currentSlot.StoreItemData.ItemData.Price);


        foreach (var item in stateButtonDict)
        {
            item.Value.gameObject.SetActive(item.Key == currentSlot.StoreItemData.IsPurchased);
        }
    }

    void BuyItem()
    {
        var dataManager = DataManager.Instance;

        var targetItemData = currentSlot.StoreItemData;

        if(targetItemData.ItemData.Price <= dataManager.PlayerData.Gold)
        {
            currentSlot.LockCoverImage.enabled = false;

            targetItemData.IsPurchased = true;

            dataManager.PlayerData.Gold -= targetItemData.ItemData.Price;


            InitNumberText(goldText, goldTextParent, goldInfoDefalutSize, dataManager.PlayerData.Gold);

            foreach (var item in stateButtonDict)
            {
                item.Value.gameObject.SetActive(item.Key == currentSlot.StoreItemData.IsPurchased);
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



    public void Init(GameEnum.ItemType itemType, List<StoreItemData> allItemDatas)
    {
        titleText.text = titleTextDict[itemType];
        equipButtonText.text = equipTextDict[itemType];

        InitNumberText(goldText, goldTextParent, goldInfoDefalutSize, DataManager.Instance.PlayerData.Gold);


        var typeCustomizeItemList = GetTypeItemList(itemType, allItemDatas);

        InitSlots(typeCustomizeItemList);
    }
}

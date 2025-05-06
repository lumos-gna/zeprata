using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    static GameManager instance;
    public static GameManager Instance => instance;

    public List<StoreItemData> StoreItemDatas { get; private set; } = new();


    [SerializeField] ItemDataTable itemDataTable;
    [SerializeField] AppearanceDataTable appearanceDataTable;
    [SerializeField] Player playerPrefab;

    Player player;
    PlayerData playerData = new();

    protected virtual void Awake()
    {
        if (instance != null)
        {
            if (instance != this)
            {
                Destroy(gameObject);
                return;
            }
        }
        else
        {
            instance = this;

            DontDestroyOnLoad(gameObject);
        }


        player = Instantiate(playerPrefab);

        playerData.statData = new()
        {
            moveSpeed = 4
        };


        var saveManager = SaveManager.Instance;

        if (saveManager.TryLoadData(out SaveData saveData))
        {
            InitLoadGame(saveData);
        }
        else
        {
            InitNewGame();
        }
    }


    void InitLoadGame(SaveData saveData)
    {
        playerData.gold = saveData.playerGold;
        playerData.tapRunnerScore = saveData.tapRunnerScore;
     
        player.Init(playerData);


        if (appearanceDataTable.TryGetAppearanceData(saveData.appearanceDataName, out AppearanceData appearanceData))
        {
            player.AppearanceController.ToggleAppearance(appearanceData);
        }

        foreach (var item in saveData.storeItems)
        {
            if (itemDataTable.TryGetItemData(item.itemName, out var itemData))
            {
                StoreItemDatas.Add(
                   new StoreItemData()
                   {
                       ItemData = itemData,
                       IsPurchased = item.isPurchased
                   });
            }
        }
    }

    void InitNewGame()
    {
        playerData.gold = 2000;

        player.Init(playerData);


        var randAppearance = appearanceDataTable.Datas[Random.Range(0, appearanceDataTable.Datas.Length)];

        player.AppearanceController.ToggleAppearance(randAppearance);



        for (int i = 0; i < itemDataTable.Datas.Length; i++)
        {
            StoreItemDatas.Add(
                new StoreItemData()
                {
                    ItemData = itemDataTable.Datas[i],
                    IsPurchased = false
                });
        }


        var saveData = SaveManager.Instance.SaveData;

        saveData.playerGold = playerData.gold;

        foreach (var item in StoreItemDatas)
        {
            saveData.storeItems.Add(
                 new StoreItemSaveData()
                 {
                     itemName = item.ItemData.ItemName,
                     isPurchased = item.IsPurchased
                 });
        }
    }
}

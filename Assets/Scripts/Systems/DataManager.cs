using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.U2D.Animation;

public class DataManager : Singleton<DataManager>
{
    public event UnityAction<AppearanceData> OnChangedAppearanceData;

    public AppearanceData AppearanceData { get; private set; }


    public int TapRunnerScore { get; set; }
    public PlayerData PlayerData { get; private set; }
    public List<StoreItemData> StoreItemDatas { get; private set; }
    public Dictionary<string, ItemData> ItemDataDict { get; private set; }
    public Dictionary<string, AppearanceData> AppearanceDataDict { get; private set; }

  


    [SerializeField] AppearanceData[] appearanceDatas;
    [SerializeField] ItemData[] itemDatas;


    protected override void Awake()
    {
        base.Awake();
    }

    public void Init()
    {
        AppearanceDataDict = appearanceDatas.ToDictionary(item => item.AppearanceName);
        ItemDataDict = itemDatas.ToDictionary(item => item.ItemName);
    }


    public void InitNewGameData()
    {
        PlayerData = new()
        {
            gold = 2000
        };

        AppearanceData = appearanceDatas[Random.Range(0, appearanceDatas.Length)];

        StoreItemDatas = new();

        foreach (var item in ItemDataDict)
        {
            StoreItemDatas.Add(
                new()
                {
                    itemName = item.Key
                });
        }
    }

    public void SetAppearanceData(AppearanceData appearanceData)
    {
        AppearanceData = appearanceData;

        OnChangedAppearanceData?.Invoke(appearanceData);
    }
}

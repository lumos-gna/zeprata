using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DataManager : Singleton<DataManager>
{

    public int TapRunnerScore { get; set; }
    public PlayerData PlayerData { get; private set; }
    public List<EquippedItemData> EquippedItemDatas { get; private set; }
    public List<StoreItemData> StoreItemDatas { get; private set; }




    public Dictionary<string, ItemData> ItemDataDict { get; private set; }

  

    [SerializeField] ItemData[] itemDatas;


    protected override void Awake()
    {
        base.Awake();
    }




    public void InitNewGameData()
    {
        PlayerData = new()
        {
            gold = 2000
        };


        StoreItemDatas = new();


        EquippedItemDatas = new();
    }
  
}

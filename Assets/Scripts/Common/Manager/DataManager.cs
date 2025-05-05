using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DataManager : Singleton<DataManager>
{
    public int TapRunnerScore { get; set; }
    public PlayerData PlayerData { get; private set; }
    public List<StoreItemData> StoreItemDatas { get; private set; } = new();



    [SerializeField] ItemDataTable itemDataTable;


    protected override void Awake()
    {
        base.Awake();
    }




    public void InitNewGameData()
    {
        PlayerData = new()
        {
            gold = 2000,
            statData = new()
            {
                moveSpeed = 4
            }
        };


        for (int i = 0; i < itemDataTable.Datas.Length; i++)
        {
            StoreItemDatas.Add(
                new StoreItemData()
                {
                    ItemData = itemDataTable.Datas[i],
                    IsPurchased = false
                }); ;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D.Animation;

public class DataManager : Singleton<DataManager>
{

    [SerializeField] List<SpriteAssetItemData> spriteAssetItemDatas;


    public int TapRunnerScore { get; set; }


    public SpriteLibraryAsset defalutPlayerSpriteAsset;


    public List<ShopItemData> ShopItemDatas { get; private set; } = new();
    public List<InventoryItemData> InventoryItemDatas { get; private set; } = new();
    public PlayerData PlayerData { get; private set; } = new();


    protected override void Awake()
    {
        base.Awake();
    }



    public void InitNewGameData()
    {
        PlayerData.Gold = 2000;
       
        for (int i = 0; i < spriteAssetItemDatas.Count; i++)
        {
            ShopItemDatas.Add(
                new ShopItemData()
                {
                    ItemData = spriteAssetItemDatas[i],
                    Count = 1
                });
        }
    }
}

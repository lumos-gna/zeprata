using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : Singleton<DataManager>
{
    [SerializeField] List<SpriteAssetItemData> spriteAssetItemDatas;

    protected override void Awake()
    {
        base.Awake();
    }

    public PlayerData PlayerData { get; private set; }

    public SpriteAssetItemData GetSpriteAssetItemData(string id)
    {
        for (int i = 0; i < spriteAssetItemDatas.Count; i++) 
        {
            if(id == spriteAssetItemDatas[i].ID)
            {
                return spriteAssetItemDatas[i];
            }
        }

        return null;
    }

    public void InitNewGameData()
    {
        PlayerData = new()
        {
            SpriteAsset = GetSpriteAssetItemData("spriteAsset_1").SpriteAsset
        };
    }
}

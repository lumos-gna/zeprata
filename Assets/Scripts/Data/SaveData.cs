
using System.Collections.Generic;

[System.Serializable]
public class SaveData
{
    public int playerGold;

    public int tapRunnerScore;

    public string appearanceDataName;

    public List<StoreItemSaveData> storeItems = new();
}
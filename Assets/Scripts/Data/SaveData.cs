
using System.Collections.Generic;

[System.Serializable]
public class SaveData
{
    public int playerGold;

    public int tapRunnerScore;

    public StatData playerStatData;

    public string appearanceDataName;

    public List<string> equippedDataNames = new();

    public List<StoreItemSaveData> storeItems = new();
}
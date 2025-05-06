using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class SaveManager :Singleton<SaveManager>
{
    public SaveData SaveData { get; private set; }


    protected override void Awake()
    {
        base.Awake();
    }

    public bool TryLoadData(out SaveData saveData)
    {
        saveData = Load();

        if(saveData != null)
        {
            SaveData = saveData;
            return true;
        }
        else
        {
            SaveData = new();
            return false;
        }
    }


    public void Save()
    {
        string path = Path.Combine(Application.persistentDataPath, "saveData.json");

        string json = JsonUtility.ToJson(SaveData, true); 

        File.WriteAllText(path, json);
    }


    SaveData Load()
    {
        string path = Path.Combine(Application.persistentDataPath, "saveData.json");

        if (!File.Exists(path))
        {
            return null;
        }

        string json = File.ReadAllText(path);

        return JsonUtility.FromJson<SaveData>(json);
    }
}

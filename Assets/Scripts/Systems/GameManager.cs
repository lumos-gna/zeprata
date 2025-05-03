using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    DataManager dataManager;
    AppearanceManager appearanceManager;


    protected override void Awake()
    {
        base.Awake();

        dataManager = DataManager.Instance;
        appearanceManager = AppearanceManager.Instance;

        dataManager.Init();

        dataManager.InitNewGameData();
    }

    private void Start()
    {
        StartNewGame();
    }


    void StartNewGame()
    {
        int rand = Random.Range(0, dataManager.AppearanceDataDict.Count);

        var targetPair = dataManager.AppearanceDataDict.ElementAt(rand);

        appearanceManager.ChangeAppearance(targetPair.Value);
    }
}

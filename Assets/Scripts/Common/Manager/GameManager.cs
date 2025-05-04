using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] AppearanceDataTable appearanceDataTable;



    protected override void Awake()
    {
        base.Awake();

        DataManager.Instance.InitNewGameData();

        StartGame(true);
    }

    void StartGame(bool isNewGame)
    {
        var player = FindAnyObjectByType<Player>();

        var randAppearance = appearanceDataTable.Datas[Random.Range(0, appearanceDataTable.Datas.Length)];


        player.Init();

        player.AppearanceController.Change(randAppearance);
    }

}

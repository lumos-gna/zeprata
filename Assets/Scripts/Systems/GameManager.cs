using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    protected override void Awake()
    {
        base.Awake();

        //세이브검사
        StartNewGame();
    }


    public void StartNewGame()
    {
        DataManager.Instance.InitNewGameData();
    }
}

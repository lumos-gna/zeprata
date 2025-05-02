using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.U2D.Animation;

public class DataManager : Singleton<DataManager>
{
    public int TapRunnerScore { get; set; }
    public PlayerData PlayerData { get; private set; } = new();
    public AppearanceData AppearanceData
    {
        get { return appearanceData; }
        set
        {
            if (appearanceData != value)
            {
                appearanceData = value;

                OnChangedAppearanceData?.Invoke(appearanceData);
            }
        }
    }
    public List<AppearanceData> AppearanceDatas => appearanceDatas;


    public event UnityAction<AppearanceData> OnChangedAppearanceData;


    [SerializeField] List<AppearanceData> appearanceDatas;


    AppearanceData appearanceData;


    protected override void Awake()
    {
        base.Awake();
    }



    public void InitNewGameData()
    {
        PlayerData.Gold = 2000;

        int rand = Random.Range(0, appearanceDatas.Count);

        AppearanceData = appearanceDatas[rand];
    }
}

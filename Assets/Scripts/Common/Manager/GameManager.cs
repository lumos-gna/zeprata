using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    static GameManager instance;
    public static GameManager Instance => instance;

    public int TapRunnerScore { get; set; }
    public PlayerData PlayerData { get; private set; }
    public List<StoreItemData> StoreItemDatas { get; private set; } = new();


    [SerializeField] ItemDataTable itemDataTable;


    protected virtual void Awake()
    {
        if (instance != null)
        {
            if (instance != this)
            {
                Destroy(gameObject);
                return;
            }
        }
        else
        {
            instance = this;

            DontDestroyOnLoad(gameObject);
        }


        StartNewGame();
    }

    void StartNewGame()
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

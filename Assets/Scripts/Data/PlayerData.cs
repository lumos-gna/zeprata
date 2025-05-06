using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D.Animation;



public class PlayerData
{
    public string name;
 
    public int gold;

    public Vector2 townPos;

    public StatData statData;

    public AppearanceData appearanceData;

    public Dictionary<GameEnum.ItemType, EquipmentItemData> equippedDatas;
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "ScriptableObjects/ItemDataTable")]
public class ItemDataTable : ScriptableObject
{
    [SerializeField] ItemData[] datas;

    public ItemData[] Datas => datas;


    public bool TryGetItemData(string targetName, out ItemData targetData)
    {
        for (int i = 0; i < datas.Length; i++) 
        {
            if (datas[i].ItemName == targetName)
            {
                targetData = datas[i];

                return true;
            }
        }

        targetData = null;

        return false;
    }
}

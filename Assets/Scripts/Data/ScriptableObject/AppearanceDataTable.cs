using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/AppearanceDataTable")]
public class AppearanceDataTable : ScriptableObject
{
    [SerializeField] AppearanceData[] datas;

    public AppearanceData[] Datas => datas;

    public bool TryGetAppearanceData(string targetName, out AppearanceData targetData)
    {
        for (int i = 0; i < datas.Length; i++)
        {
            if (datas[i].DataName == targetName)
            {
                targetData = datas[i];

                return true;
            }
        }

        targetData = null;

        return false;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/AppearanceDataTable")]
public class AppearanceDataTable : ScriptableObject
{
    [SerializeField] AppearanceData[] appearanceDatas;
}

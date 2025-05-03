using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "ScriptableObjects/ItemDataTable")]
public class ItemDataTable : ScriptableObject
{
    [SerializeField] ItemData[] itemDatas;


}

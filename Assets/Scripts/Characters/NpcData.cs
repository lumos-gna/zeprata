using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "ScriptableObject/NpcData")]
public class NpcData : ScriptableObject
{
    [SerializeField] private string npcName;
}

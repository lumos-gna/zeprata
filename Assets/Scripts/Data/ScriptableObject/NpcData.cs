using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "ScriptableObjects/NpcData")]
public class NpcData : ScriptableObject
{
    [SerializeField] private string npcName;
}

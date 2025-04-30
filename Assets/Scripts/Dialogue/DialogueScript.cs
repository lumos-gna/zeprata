using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "ScriptableObject/DialogueScript")]
public class DialogueScript : ScriptableObject
{
    public List<DialogueData> dialogueList;
}

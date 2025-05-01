using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


[CreateAssetMenu(menuName = "ScriptableObjects/DialogueData")]
public class DialogueData : ScriptableObject
{
    [SerializeField] List<DialogueScript> dialogueList;

    public List<DialogueScript> DialogueList => dialogueList;
    public UnityAction OnFinishDialogue { get; set; }
}

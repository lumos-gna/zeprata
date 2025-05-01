using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class DialogueSelect
{
    [SerializeField] string text;
    public UnityAction OnSelect { get; set; }
}

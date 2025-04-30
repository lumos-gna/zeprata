using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : Singleton<UIManager>
{

    [SerializeField] DialogueUI dialogueUIPrefab;

    public DialogueUI DialogueUI { get; private set; }


    protected override void Awake()
    {
        base.Awake();
    }


    public void EnableDialogue(DialogueData dialogueData)
    {
        InputManager.Instance.SwitchInputType(GameEnum.InputType.Dialogue);

        if(DialogueUI == null)
        {
            DialogueUI = Instantiate(dialogueUIPrefab);
        }

        DialogueUI.EnableDialogue(dialogueData);
    }
}

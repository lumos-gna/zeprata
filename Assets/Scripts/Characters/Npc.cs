using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Npc : MonoBehaviour, IInteractable
{

    [SerializeField] NpcData npcData;
    [SerializeField] DialogueData dialogueData;


    public void Interact(GameObject source)
    {
        UIManager.Instance.EnableDialogue(dialogueData);
        InputManager.Instance.SwitchInputType(GameEnum.InputType.Dialogue);
    }
}

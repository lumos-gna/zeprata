using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(CapsuleCollider2D))]
public class Npc : MonoBehaviour, IInteractable
{

    [SerializeField] NpcData npcData;
    [SerializeField] DialogueData dialogueData;


    public void Interact(Player player)
    {
        DialogueManager.Instance.EnableDialogue(dialogueData);
        InputManager.Instance.SwitchInputType(GameEnum.InputType.Dialogue);
    }
}

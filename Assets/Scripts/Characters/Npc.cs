using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(CapsuleCollider2D))]
public class Npc : MonoBehaviour, ITriggerable, IInteractable
{
    SpriteRenderer spriteRenderer;

    [SerializeField] GameObject activeSign;
    [SerializeField] Canvas dialogueCanvas;
    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        activeSign.SetActive(false);
    }

    
    
    void ITriggerable.OnEnter(Player player)
    {
        activeSign.SetActive(true);
    }

    void ITriggerable.OnExit(Player player)
    {
        activeSign.SetActive(false);
    }
    

    void IInteractable.Start(Player player)
    {
        dialogueCanvas.enabled = true;
    }

    void IInteractable.Next(Player player)
    {
        dialogueCanvas.enabled = true;
    }

    void IInteractable.Finish(Player player)
    {
        dialogueCanvas.enabled = false;
    }
}

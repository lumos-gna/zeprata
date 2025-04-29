using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(CapsuleCollider2D))]
public class Npc : MonoBehaviour, ITriggerable<Player>, IInteractable
{
    SpriteRenderer spriteRenderer;

    [SerializeField] GameObject activeSign;
    [SerializeField] Canvas dialogueCanvas;
    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        activeSign.SetActive(false);
    }

    public void OnEnter(Player player)
    {
        activeSign.SetActive(true);
        player.InteractTarget = this;
    }

    public void OnExit(Player player)
    {
        activeSign.SetActive(false);
        player.InteractTarget = null;
    }
    public void OnStay(Player player){}


    public void InteractStart(Player player)
    {
        dialogueCanvas.enabled = true;
    }

    public void InteractEnd(Player player)
    {
        dialogueCanvas.enabled = false;
    }
}

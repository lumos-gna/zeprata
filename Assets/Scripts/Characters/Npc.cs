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
    

    

    void IInteractable.Start(Player player)
    {
        dialogueCanvas.enabled = true;
        player.InputHandler.SwitchType(GameEnum.InputType.Dialogue);
    }
    
    void IInteractable.Next(Player player)
    {
        Debug.Log("대화 진행");  
    }
    
    void IInteractable.End(Player player)
    {
        dialogueCanvas.enabled = false;
        player.InputHandler.SwitchType(GameEnum.InputType.Main);
    }
}

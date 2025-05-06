using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Npc : MonoBehaviour, ITriggerEventable, IInteractable
{
    [SerializeField] NpcData npcData;
    [SerializeField] DialogueData dialogueData;

    [Space(10f)]
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] Canvas canvas;


    void Flip(GameObject target)
    {
        float targetDir = target.transform.position.x - transform.position.x;

        if(targetDir < 0)
        {
            spriteRenderer.flipX = true;
        }
        else if(targetDir > 0) 
        {
            spriteRenderer.flipX = false;
        }
    }

    public void OnTriggerEntered(GameObject source)
    {
        Flip(source);
        canvas.enabled = true;
    }

    public void OnTriggerExited(GameObject source)
    {
        canvas.enabled = false;
    }

    public void Interact(GameObject source)
    {
    }
}

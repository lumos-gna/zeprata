using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Npc : MonoBehaviour
{
    [SerializeField] SpriteRenderer spriteRenderer;

    [SerializeField] NpcData npcData;
    [SerializeField] InteractTrigger interactTrigger;
    [SerializeField] Canvas canvas;

    [SerializeField] protected DialogueData dialogueData;


    protected virtual void Awake()
    {
        interactTrigger.OnInteract = (source) =>
        {
            if (dialogueData != null)
            {
                //UIManager.Instance.EnableDialogue(dialogueData);
            }
        };

        interactTrigger.OnTriggerEnter = (source) =>
        {
            TryFilp(source);
            canvas.enabled = true;
        };

        interactTrigger.OnTriggerEixt = (source) => canvas.enabled = false;
    }

  

    void TryFilp(GameObject target)
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
}

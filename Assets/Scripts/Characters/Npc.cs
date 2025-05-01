using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Npc : MonoBehaviour
{
    [SerializeField] SpriteRenderer spriteRenderer;

    [SerializeField] NpcData npcData;
    [SerializeField] DialogueData dialogueData;
    [SerializeField] InteractTrigger interactTrigger;
    [SerializeField] Canvas canvas;


    void Start()
    {
        interactTrigger.OnInteract = (source) =>
        {
            UIManager.Instance.EnableDialogue(dialogueData);
            InputManager.Instance.SwitchInputType(GameEnum.InputType.Dialogue);
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

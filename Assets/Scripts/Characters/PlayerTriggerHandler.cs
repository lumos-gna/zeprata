using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerTriggerHandler : MonoBehaviour
{
    Player player;

    IInteractable interactTarget;
    public IInteractable InteractTarget => interactTarget;
    

    private void Awake()
    {
        player = GetComponent<Player>();
    }


    void TryTriggerPlayer<T>(T target, UnityAction<ITriggerable> onTrigger) where T : Component
    {
        if (target.TryGetComponent(out ITriggerable triggerTarget))
        {
            onTrigger?.Invoke(triggerTarget);
        }
    }

    void CheckInteractTarget<T>(T target) where T : Component
    {
        if (target.TryGetComponent(out IInteractable interactable))
        {
            interactTarget = interactable;
        }
    }
    
    
    void OnCollisionEnter2D(Collision2D other)
    {
        TryTriggerPlayer(other.collider, (target) => target.OnEnter(player));
        CheckInteractTarget(other.collider);
    }

    void OnCollisionExit2D(Collision2D other)
    {
        TryTriggerPlayer(other.collider, (target) => target.OnEnter(player));
        CheckInteractTarget(other.collider);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        TryTriggerPlayer(other, (target) => target.OnEnter(player));
        CheckInteractTarget(other);
    }

    void OnTriggerExit2D(Collider2D other)
    {
        TryTriggerPlayer(other, (target) => target.OnEnter(player));
        CheckInteractTarget(other);
    }
}

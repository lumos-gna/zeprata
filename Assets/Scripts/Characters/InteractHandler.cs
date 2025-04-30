using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractHandler : MonoBehaviour
{

    IInteractable interactTarget;
    public IInteractable InteractTarget => interactTarget;
    
    void AddInteractTarget(Collider2D collider)
    {
        if(interactTarget == null)
        {
            if (collider.TryGetComponent(out IInteractable interactable))
            {
                interactTarget = interactable;
            }
        }
    }

    void RemoveInteractTarget(Collider2D collider)
    {
        if (interactTarget != null)
        {
            if (collider.TryGetComponent(out IInteractable interactable))
            {
                if (interactTarget == interactable)
                {
                    interactTarget = null;
                }
            }
        }
    }
    
    void OnCollisionEnter2D(Collision2D other)
    {
        AddInteractTarget(other.collider);

    }

    void OnCollisionExit2D(Collision2D other)
    {
        RemoveInteractTarget(other.collider);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        AddInteractTarget(other);
    }

    void OnTriggerExit2D(Collider2D other)
    {
        RemoveInteractTarget(other);
    }
}

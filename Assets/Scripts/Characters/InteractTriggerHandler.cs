using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractTriggerHandler : MonoBehaviour
{
    IInteractTriggerable interactTarget;
    public IInteractTriggerable InteractTarget => interactTarget;
    

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out IInteractTriggerable interactable))
        {
            if (interactTarget == null)
            {
                interactTarget = interactable;

                interactTarget.TriggerEnter(gameObject);
            }
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.TryGetComponent(out IInteractTriggerable interactable))
        {
            if (interactTarget == interactable)
            {
                interactTarget.TriggerExit(gameObject);

                interactTarget = null;
            }
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


[RequireComponent(typeof(Collider2D))]
public class InteractTriggerHandler : MonoBehaviour
{
    GameObject source;

    IInteractTriggerable interactTarget;
    public IInteractTriggerable InteractTarget => interactTarget;

    public event UnityAction OnTriggerEnter;
    public event UnityAction OnTriggerEixt;

    public void Init(GameObject source)
    {
        this.source = source;
    }
    

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out IInteractTriggerable interactable))
        {
            if (interactTarget == null)
            {
                interactTarget = interactable;

                interactTarget.TriggerEnter(source);

                OnTriggerEnter?.Invoke();
            }
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.TryGetComponent(out IInteractTriggerable interactable))
        {
            if (interactTarget == interactable)
            {
                interactTarget.TriggerExit(source);

                interactTarget = null;

                OnTriggerEixt?.Invoke();
            }
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class TriggerEventController : MonoBehaviour
{
    public event UnityAction<ITriggerEventable> OnTriggerEnter;
    public event UnityAction<ITriggerEventable> OnTriggerExit;


    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out ITriggerEventable eventTarget))
        {
            OnTriggerEnter?.Invoke(eventTarget);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.TryGetComponent(out ITriggerEventable eventTarget))
        {
            OnTriggerExit?.Invoke(eventTarget);
        }
    }
}

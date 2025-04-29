using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerTriggerHandler : MonoBehaviour
{
    public void TryTriggerPlayer<T>(T target, UnityAction<ITriggerable<Player>> onTrigger) where T : Component
    {
        if (target.TryGetComponent(out ITriggerable<Player> triggerTarget))
        {
            onTrigger?.Invoke(triggerTarget);
        }
    }
}

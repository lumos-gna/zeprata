using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerTriggerHandler : MonoBehaviour
{
    Player player;
    

    private void Awake()
    {
        player = GetComponent<Player>();
    }


    public void TryTriggerPlayer<T>(T target, UnityAction<ITriggerable<Player>> onTrigger) where T : Component
    {
        if (target.TryGetComponent(out ITriggerable<Player> triggerTarget))
        {
            onTrigger?.Invoke(triggerTarget);
        }
    }
    
    
    void OnCollisionEnter2D(Collision2D other)  => TryTriggerPlayer(other.collider, (target) => target.OnEnter(player));
    void OnCollisionStay2D(Collision2D other) => TryTriggerPlayer(other.collider, (target) => target.OnStay(player));
    void OnCollisionExit2D(Collision2D other)  => TryTriggerPlayer(other.collider, (target) => target.OnExit(player));


    void OnTriggerEnter2D(Collider2D other) => TryTriggerPlayer(other, (target) => target.OnEnter(player));
    void OnTriggerStay2D(Collider2D other) => TryTriggerPlayer(other, (target) => target.OnStay(player));
    void OnTriggerExit2D(Collider2D other)  => TryTriggerPlayer(other, (target) => target.OnExit(player));
}

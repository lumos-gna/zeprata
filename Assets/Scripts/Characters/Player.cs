using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CapsuleCollider2D))]
[RequireComponent(typeof(PlayerTriggerHandler))]
[RequireComponent(typeof(PlayerMoveHandler))]
public class Player : MonoBehaviour
{
    Vector2 moveDir;
    Rigidbody2D rigidbody;
    PlayerTriggerHandler triggerHandler;
    PlayerMoveHandler moveHandler;
    PlayerInput inputHandler;

    public IInteractable InteractTarget { get; set; }

    void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        
        triggerHandler = GetComponent<PlayerTriggerHandler>();
        moveHandler = GetComponent<PlayerMoveHandler>();
        inputHandler = GetComponent<PlayerInput>();
    }

    
    void FixedUpdate()
    {
        moveHandler.FixedMove(moveDir, rigidbody);
    }


    
    void OnMove(InputValue value) => moveDir = value.Get<Vector2>().normalized;

    void OnInteract()
    {
        if (InteractTarget != null)
        {
            InteractTarget.InteractStart(this);
            inputHandler.SwitchCurrentActionMap("Dialogue");
        }
    }


    void OnEnd()
    {
        if (InteractTarget != null)
        {
            InteractTarget.InteractEnd(this);
            inputHandler.SwitchCurrentActionMap("Main");
        }
    }
    
    
    void OnCollisionEnter2D(Collision2D other)  => triggerHandler.TryTriggerPlayer(other.collider, (target) => target.OnEnter(this));
    void OnCollisionExit2D(Collision2D other)  => triggerHandler.TryTriggerPlayer(other.collider, (target) => target.OnExit(this));
    void OnCollisionStay2D(Collision2D other) => triggerHandler.TryTriggerPlayer(other.collider, (target) => target.OnStay(this));


    void OnTriggerEnter2D(Collider2D other) => triggerHandler.TryTriggerPlayer(other, (target) => target.OnEnter(this));
    void OnTriggerExit2D(Collider2D other)  => triggerHandler.TryTriggerPlayer(other, (target) => target.OnExit(this));
    void OnTriggerStay2D(Collider2D other) => triggerHandler.TryTriggerPlayer(other, (target) => target.OnStay(this));
}

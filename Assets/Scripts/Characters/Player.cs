using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CapsuleCollider2D))]
[RequireComponent(typeof(PlayerTriggerHandler))]
[RequireComponent(typeof(PlayerInputHandler))]
[RequireComponent(typeof(PlayerMoveHandler))]
public class Player : MonoBehaviour
{
    private Vector2 inputPos;
    private Rigidbody2D rigidbody;
    private PlayerTriggerHandler triggerHandler;
    private PlayerInputHandler inputHandler;
    private PlayerMoveHandler moveHandler;
    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        
        triggerHandler = GetComponent<PlayerTriggerHandler>();
        moveHandler = GetComponent<PlayerMoveHandler>();
        inputHandler = GetComponent<PlayerInputHandler>();
    }

    
    private void FixedUpdate()
    {
        moveHandler.FixedMove(inputPos, rigidbody);
    }

    private void Update()
    {
        inputPos = inputHandler.GetMoveInput();
    }
    
    
    private void OnCollisionEnter2D(Collision2D other)  => triggerHandler.TryTriggerPlayer(other.collider, (target) => target.OnEnter(this));
    private void OnCollisionExit2D(Collision2D other)  => triggerHandler.TryTriggerPlayer(other.collider, (target) => target.OnExit(this));
    private void OnCollisionStay2D(Collision2D other) => triggerHandler.TryTriggerPlayer(other.collider, (target) => target.OnStay(this));


    private void OnTriggerEnter2D(Collider2D other) => triggerHandler.TryTriggerPlayer(other, (target) => target.OnEnter(this));
    private void OnTriggerExit2D(Collider2D other)  => triggerHandler.TryTriggerPlayer(other, (target) => target.OnExit(this));
    private void OnTriggerStay2D(Collider2D other) => triggerHandler.TryTriggerPlayer(other, (target) => target.OnStay(this));
}

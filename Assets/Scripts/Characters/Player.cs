using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CapsuleCollider2D))]
[RequireComponent(typeof(PlayerTriggerHandler))]
[RequireComponent(typeof(PlayerMoveHandler))]
public class Player : MonoBehaviour
{
    Rigidbody2D rigidbody;
    PlayerTriggerHandler triggerHandler;
    PlayerMoveHandler moveHandler;
    PlayerInputHandler inputHandler;
    public PlayerInputHandler InputHandler => inputHandler;

    public IInteractable InteractTarget { get; set; }

    void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        triggerHandler = GetComponent<PlayerTriggerHandler>();
        moveHandler = GetComponent<PlayerMoveHandler>();
        inputHandler = GetComponent<PlayerInputHandler>();
    }

    void FixedUpdate()
    {
        moveHandler.FixedMove(inputHandler.MoveDir, rigidbody);
    }
}

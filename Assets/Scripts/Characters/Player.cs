using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CapsuleCollider2D))]
[RequireComponent(typeof(PlayerTriggerHandler))]
[RequireComponent(typeof(PlayerMoveHandler))]
[RequireComponent(typeof(PlayerInputHandler))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(SpriteRenderer))]
public class Player : MonoBehaviour
{
    Rigidbody2D rigidbody;
    SpriteRenderer spriteRenderer;
    Animator animator;
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
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void FixedUpdate()
    {
        if(inputHandler.MoveDir != Vector2.zero)
        {
            moveHandler.FixedMove(inputHandler.MoveDir, rigidbody);

            animator.SetBool("isMove", true);

            if(inputHandler.MoveDir.x < 0)
            {
                spriteRenderer.flipX = true;
            }
            else if(inputHandler.MoveDir.x > 0)
            {
                spriteRenderer.flipX = false;
            }
         
        }
        else
        {
            animator.SetBool("isMove", false);
        }
    }
}

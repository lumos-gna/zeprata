using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


[RequireComponent(typeof(Rigidbody2D))]

[RequireComponent(typeof(CapsuleCollider2D))]
[RequireComponent(typeof(PlayerTriggerHandler))]
[RequireComponent(typeof(PlayerMoveHandler))]
[RequireComponent(typeof(PlayerInputHandler))]
[RequireComponent(typeof(PlayerSpriteHandler))]
public class Player : MonoBehaviour
{
    Rigidbody2D rigidbody;

    PlayerTriggerHandler triggerHandler;
    PlayerMoveHandler moveHandler;
    PlayerSpriteHandler spriteHandler;
    PlayerInputHandler inputHandler;

    public PlayerInputHandler InputHandler => inputHandler;


    void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();

        triggerHandler = GetComponent<PlayerTriggerHandler>();
        moveHandler = GetComponent<PlayerMoveHandler>();
        spriteHandler = GetComponent<PlayerSpriteHandler>();
        inputHandler = GetComponent<PlayerInputHandler>();
    }

    void FixedUpdate()
    {
        Move(inputHandler.MoveKey);
    }


    void Move(Vector2 moveKey)
    {
        Vector2 moveDir =  moveKey.normalized;

        moveHandler.FixedMove(moveDir, rigidbody);

        spriteHandler.Move(moveDir != Vector2.zero);

        spriteHandler.Flip(moveDir.x < 0);
    }

    public void StartInteract()
    {
        triggerHandler.InteractTarget.Start(this);
        inputHandler.SwitchType(GameEnum.InputType.Interact);
    }
    public void FinishInteract()
    {
        triggerHandler.InteractTarget.Finish(this);
        inputHandler.SwitchType(GameEnum.InputType.Main);
    }

    public void NextInteract()
    {
        triggerHandler.InteractTarget.Next(this);
    }

    public void Jump() => spriteHandler.Jump();
}

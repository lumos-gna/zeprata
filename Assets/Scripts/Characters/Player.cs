using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CapsuleCollider2D))]
[RequireComponent(typeof(PlayerTriggerHandler))]
[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Animator))]

public class Player : MonoBehaviour
{
    bool isJumping = false;

    Vector2 moveDir;

    Rigidbody2D rigid;

    SpriteRenderer spriteRenderer;

    Animator animator;

    PlayerTriggerHandler triggerHandler;

    InputManager inputManager;


    [SerializeField] float moveSpeed;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        triggerHandler = GetComponent<PlayerTriggerHandler>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();


        inputManager = InputManager.Instance;

        inputManager.OnMoveEvent += (moveKey) => moveDir = moveKey.normalized;

        inputManager.OnJumpEvent += Jump;

        inputManager.OnInteractEvent += () => triggerHandler.InteractTarget.Interact(this);
    }


    void FixedUpdate()
    {
        Move();
    }


    void Move()
    {
        rigid.MovePosition(rigid.position + (moveDir * moveSpeed * Time.fixedDeltaTime));

        spriteRenderer.flipX = moveDir.x < 0;

        animator.SetBool("isMove", moveDir != Vector2.zero);
    }


    void Jump()
    {
        if (!isJumping)
        {
            isJumping = true;

            animator.SetBool("isJump", isJumping);

            StartCoroutine(FinishJump());
        }
    }

    IEnumerator FinishJump()
    {
        yield return new WaitForSeconds(0.33f);

        isJumping = false;
        animator.SetBool("isJump", isJumping);
    }
}

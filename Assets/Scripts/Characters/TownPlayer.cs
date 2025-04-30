using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.U2D.Animation;


[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CapsuleCollider2D))]
[RequireComponent(typeof(InteractHandler))]
[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(SpriteLibrary))]
[RequireComponent(typeof(Animator))]

public class TownPlayer : MonoBehaviour
{
    bool isJumping = false;

    Vector2 moveDir;

    Rigidbody2D rigid;
    SpriteRenderer spriteRenderer;
    SpriteLibrary spriteLibrary;
    Animator animator;
    InteractHandler interactHandler;
    PlayerData playerData;

    [SerializeField] float moveSpeed;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        interactHandler = GetComponent<InteractHandler>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        spriteLibrary = GetComponent<SpriteLibrary>();
    }


    public void Init(PlayerData playerData)
    {
        this.playerData = playerData;

        spriteLibrary.spriteLibraryAsset = playerData.SpriteAsset;


        var inputManager = InputManager.Instance;

        inputManager.OnMoveEvent += (moveKey) => moveDir = moveKey.normalized;

        inputManager.OnJumpEvent += Jump;

        inputManager.OnInteractEvent += () => interactHandler.InteractTarget.Interact(gameObject);
    }


    void FixedUpdate()
    {
        Move();
    }


    void Move()
    {
        rigid.MovePosition(rigid.position + (moveDir * moveSpeed * Time.fixedDeltaTime));

        bool isMove = moveDir != Vector2.zero;

        animator.SetBool("isMove", isMove);

        if (isMove)
        {
            spriteRenderer.flipX = moveDir.x < 0;
        }
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

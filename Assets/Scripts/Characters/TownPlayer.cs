using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.U2D.Animation;



public class TownPlayer : MonoBehaviour
{
    bool isJumping = false;

    Vector2 moveDir;

    PlayerData playerData;

    [SerializeField] Rigidbody2D rigid;
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] SpriteLibrary spriteLibrary;
    [SerializeField] Animator animator;
    [SerializeField] InteractTriggerHandler triggerHandler;

    [SerializeField] Canvas interactGuideCanvas;

    [SerializeField] float moveSpeed;



    public void Init(PlayerData playerData)
    {
        this.playerData = playerData;

        spriteLibrary.spriteLibraryAsset = playerData.SpriteAsset;


        var inputManager = InputManager.Instance;

        inputManager.OnMoveEvent = (moveKey) => moveDir = moveKey.normalized;

        inputManager.OnJumpEvent = Jump;

        inputManager.OnInteractEvent = () => triggerHandler.InteractTarget?.Interact(gameObject);

        triggerHandler.Init(gameObject);

        triggerHandler.OnTriggerEnter = () => interactGuideCanvas.enabled = true;
        triggerHandler.OnTriggerEixt = () => interactGuideCanvas.enabled = false;
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

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.U2D.Animation;



public class TownPlayer : MonoBehaviour
{
    [SerializeField] float moveSpeed;

    [SerializeField] Rigidbody2D rigid;

    [SerializeField] SpriteRenderer spriteRenderer;

    [SerializeField] SpriteLibrary spriteLibrary;

    [SerializeField] Animator animator;

    [SerializeField] InteractTriggerHandler triggerHandler;

    [SerializeField] Canvas interactGuideCanvas;



    bool isJumping = false;

    Vector2 moveDir;



    void FixedUpdate()
    {
        Move();
    }


    private void OnEnable()
    {
        AppearanceManager.Instance.OnChangeAppearance += (data) => InitSpriteLibrary(data.LibraryAsset);
    }

    private void OnDisable()
    {
        AppearanceManager.Instance.OnChangeAppearance -= (data) => InitSpriteLibrary(data.LibraryAsset);
    }



    public void Init()
    {
        var inputManager = InputManager.Instance;

        inputManager.OnMoveEvent = (moveKey) => moveDir = moveKey.normalized;

        inputManager.OnJumpEvent = Jump;

        inputManager.OnInteractEvent = () =>
        {
            if (triggerHandler.InteractTarget != null)
            {
                triggerHandler.InteractTarget?.Interact(gameObject);
                interactGuideCanvas.enabled = false;
            }
        };


        triggerHandler.Init(gameObject);

        triggerHandler.OnTriggerEnter += () => interactGuideCanvas.enabled = true;

        triggerHandler.OnTriggerEixt += () => interactGuideCanvas.enabled = false;
    }


    void InitSpriteLibrary(SpriteLibraryAsset asset) => spriteLibrary.spriteLibraryAsset = asset;




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

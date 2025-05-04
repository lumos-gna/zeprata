using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player : MonoBehaviour
{
    public InputController InputController => inputController;
    public AppearanceController AppearanceController => appearanceController;


    [SerializeField] float moveSpeed;

    [SerializeField] Canvas interactGuideCanvas;


    [Space(10f)]
    [Header("Component")]

    [SerializeField] Rigidbody2D rigid;

    [SerializeField] Animator animator;

    [SerializeField] ObjectSpriteRendererController rendererController;

    [SerializeField] AppearanceController appearanceController;

    [SerializeField] EquipmentController equipmentController;

    [SerializeField] InputController inputController;

    [SerializeField] InteractTriggerHandler triggerController;


    bool isJumping = false;

    Vector2 moveDir;

    Vector2 characterRendererDefalutPos;


    void FixedUpdate()
    {
        Move();
    }


    public void Init()
    {
        if (rendererController.TryGetRenderer("Characetr", out ObjectSpriteRenderer target))
        {
            characterRendererDefalutPos = target.transform.position;
        }


        appearanceController.OnChangeEvent += (data) => rendererController.ChangeLibraryAsset(data.Type.ToString(), data.LibraryAsset);


        equipmentController.OnEquippedItem += (data) =>
        {
            switch (data)
            {
                case RidingItemData ridingItemData: ToggleMount(true, ridingItemData); break;
            }
        };
        equipmentController.OnUnEquippedItem += (data) =>
        {
            switch (data)
            {
                case RidingItemData ridingItemData: ToggleMount(false, ridingItemData); break;
            }
        };

        triggerController.Init(gameObject);

        triggerController.OnTriggerEnter += () => interactGuideCanvas.enabled = true;

        triggerController.OnTriggerEixt += () => interactGuideCanvas.enabled = false;



        inputController.OnMoveEvent += (moveKey) => moveDir = moveKey.normalized;

        inputController.OnJumpEvent += Jump;

        inputController.OnInteractEvent += () =>
        {
            if (triggerController.InteractTarget != null)
            {
                triggerController.InteractTarget?.Interact(gameObject);
                interactGuideCanvas.enabled = false;
            }
        };


        DontDestroyOnLoad(gameObject);
    }



    void ToggleMount(bool isMount, RidingItemData data)
    {
        animator.SetBool("isRide", isMount);

        if (rendererController.TryGetRenderer("Characetr", out ObjectSpriteRenderer target))
        {
            target.transform.position = isMount ? data.MountPoint : characterRendererDefalutPos;
        }
    }

 

    void Move()
    {
        rigid.MovePosition(rigid.position + (moveDir * moveSpeed * Time.fixedDeltaTime));

        bool isMove = moveDir != Vector2.zero;

        animator.SetBool("isMove", isMove);

        if (isMove)
        {
            rendererController.SetRenderersFlipX(moveDir.x < 0);
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

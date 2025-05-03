using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.VersionControl;
using UnityEngine;



public class TownPlayer : MonoBehaviour
{
    [SerializeField] float moveSpeed;

    [SerializeField] Rigidbody2D rigid;

    [SerializeField] Animator animator;

    [SerializeField] InteractTriggerHandler triggerHandler;


    [SerializeField] Canvas interactGuideCanvas;

    [SerializeField] ObjectSpriteRendererHandler rendererHandler;

    [SerializeField] AppearanceHandler appearanceHandler;

    [SerializeField] EquipmentHandler equipmentHandler;

    bool isJumping = false;

    Vector2 moveDir;


    private void Awake()
    {
        appearanceHandler.OnChangeEvent += (data) => rendererHandler.ChangeLibraryAsset(data.Type.ToString(), data.LibraryAsset);

        equipmentHandler.OnEquippedItem += (data) => Equip(data);
        equipmentHandler.OnUnEquippedItem += (data) => UnEquip(data);
    }


    void FixedUpdate()
    {
        Move();
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


    void Equip(EquipmentItemData itemData)
    {
        switch(itemData)
        {
            case RidingItemData data:
                animator.SetBool("isRide", true);
                //플레이어 위치를 ridingData 에 맞게 수정 로직
                break;
        }
    }

    void UnEquip(EquipmentItemData itemData)
    {
        switch (itemData)
        {
            case RidingItemData:
                animator.SetBool("isRide", false);
                break;
        }

    }



    void Move()
    {
        rigid.MovePosition(rigid.position + (moveDir * moveSpeed * Time.fixedDeltaTime));

        bool isMove = moveDir != Vector2.zero;

        animator.SetBool("isMove", isMove);

        if (isMove)
        {
            rendererHandler.SetRenderersFlipX(moveDir.x < 0);
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

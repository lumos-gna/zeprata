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

    [SerializeField] float moveSpeed;

    [SerializeField] Rigidbody2D rigid;

    [SerializeField] SpriteRenderer spriteRenderer;

    [SerializeField] SpriteLibrary spriteLibrary;

    [SerializeField] Animator animator;

    [SerializeField] InteractTriggerHandler triggerHandler;

    [SerializeField] Canvas interactGuideCanvas;



    void FixedUpdate()
    {
        Move();
    }



    void OnDestroy()
    {
        var inventoryManager = InventoryManager.Instance;

        inventoryManager.OnEquippedItemData -= (itemData) => InitItemSpriteAsset(itemData);
        inventoryManager.OnUnEquippedItemData -= (itemData) => InitDefalutSpriteAsset();
    }


    void InitItemSpriteAsset(ItemData itemData)
    {
        if (itemData is SpriteAssetItemData spriteAssetItemData)
        {
            spriteLibrary.spriteLibraryAsset = spriteAssetItemData.SpriteAsset;
        }
    }

    void InitDefalutSpriteAsset() => spriteLibrary.spriteLibraryAsset = DataManager.Instance.DefalutPlayerSpriteAsset;
   

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



    public void Init()
    {
        var inventoryManager = InventoryManager.Instance;
        var dataManager = DataManager.Instance;
        var inputManager = InputManager.Instance;


        if (inventoryManager.TryGetEquipItemData(out SpriteAssetItemData itemData))
        {
            InitItemSpriteAsset(itemData);
        }
        else
        {
            InitDefalutSpriteAsset();
        }

        inventoryManager.OnEquippedItemData += (itemData) => InitItemSpriteAsset(itemData);
        inventoryManager.OnUnEquippedItemData += (itemData) => InitDefalutSpriteAsset();



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


}

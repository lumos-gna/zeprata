using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player : MonoBehaviour
{
    public PlayerData Data => data;
    public InputController InputController => inputController;
    public AppearanceController AppearanceController => appearanceController;
    public EquipmentController EquipmentController => equipmentController;  


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

    float mountDefalutHeight;

    Vector2 moveDir;


    PlayerData data;

    DataManager dataManager;


    void FixedUpdate()
    {
        Move();
    }


    public void Init()
    {
        dataManager = DataManager.Instance;

        data = dataManager.PlayerData;


        if (rendererController.TryGetRenderer("Characetr", out ObjectSpriteRenderer target))
        {
            mountDefalutHeight = target.transform.localPosition.y;
        }


        appearanceController.OnToggleAppearanceEvent += (appearanceData) =>
        {
            rendererController.ChangeLibraryAsset(appearanceData.Type.ToString(), appearanceData.LibraryAsset);
        };

        equipmentController.Init(new(), data.statData);

        equipmentController.OnToggleEquipEvent += (isEquip, targetData) =>
        {
            var changedAsset = isEquip ? targetData.SpriteAsset : null;

            rendererController.ChangeLibraryAsset(targetData.Type.ToString(), changedAsset);

            switch (targetData)
            {
                case RidingItemData ridingItemData: ToggleMount(isEquip, ridingItemData); break;
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
        animator.SetBool("isMount", isMount);

        if (rendererController.TryGetRenderer("Character", out ObjectSpriteRenderer character))
        {
            Vector2 tempLocalPos = character.transform.localPosition;

            tempLocalPos.y = isMount ? data.MountHeight : mountDefalutHeight;

            character.transform.localPosition = tempLocalPos;
        }

        if (rendererController.TryGetRenderer("Riding", out ObjectSpriteRenderer riding))
        {
            riding.gameObject.SetActive(isMount);
        }
    }

 

    void Move()
    {
        rigid.MovePosition(rigid.position + (moveDir * data.statData.moveSpeed * Time.fixedDeltaTime));

        float speed = moveDir != Vector2.zero ? data.statData.moveSpeed : 0;

        animator.SetFloat("Speed", speed);

        if (speed > 0 && moveDir.x != 0)
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

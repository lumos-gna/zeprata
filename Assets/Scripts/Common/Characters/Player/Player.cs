using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class Player : MonoBehaviour
{
    public PlayerData Data => data;
    public Rigidbody2D Rigid => rigid;
    public Animator Animator => animator;

    public InputController InputController => inputController;
    public AppearanceController AppearanceController => appearanceController;
    public EquipmentController EquipmentController => equipmentController;  
    public CollisionEventController CollisionEventController => collisionEventController;
    public ObjectSpriteRendererController RendererController => rendererController;


    [SerializeField] Canvas interactGuideCanvas;


    [Space(10f)]
    [Header("Component")]

    [SerializeField] Rigidbody2D rigid;

    [SerializeField] Animator animator;

    [SerializeField] PlayerInput playerInput;

    [SerializeField] PlayerMovementController movementController;

    [SerializeField] PlayerRidingController ridingController;

    [SerializeField] ObjectSpriteRendererController rendererController;

    [SerializeField] AppearanceController appearanceController;

    [SerializeField] EquipmentController equipmentController;

    [SerializeField] InputController inputController;

    [SerializeField] TriggerEventController triggerEventController;

    [SerializeField] CollisionEventController collisionEventController;


    PlayerData data;

    GameManager gameManager;

    IInteractable interactTarget;


    private void Awake()
    {
        gameManager = GameManager.Instance;

        data = gameManager.PlayerData;


        ridingController.Init(animator, rendererController);

        movementController.Init(rigid, animator, rendererController);

        InitAppearacneController();

        InitEquipmentController();

        InitTriggerController();

        InitCollisionController();

        InitInputController();

        DontDestroyOnLoad(gameObject);
    }


    void FixedUpdate()
    {
        movementController.Move(data.statData.moveSpeed);
    }



    void InitAppearacneController()
    {
        appearanceController.OnToggleAppearanceEvent += (appearanceData) =>
        {
            rendererController.ChangeLibraryAsset(appearanceData.Type.ToString(), appearanceData.LibraryAsset);
        };
    }


    void InitEquipmentController()
    {
        equipmentController.Init(data.statData);

        equipmentController.OnToggleEquipEvent += (isEquip, targetData) =>
        {
            var changedAsset = isEquip ? targetData.SpriteAsset : null;

            rendererController.ChangeLibraryAsset(targetData.Type.ToString(), changedAsset);

            switch (targetData)
            {
                case RidingItemData ridingItemData: ridingController.ToggleMount(isEquip, ridingItemData); break;
            }
        };
    }


    void InitTriggerController()
    {
        triggerEventController.OnTriggerEnter += (target) =>
        {
            target.OnTriggerEntered(gameObject);

            if (target is IInteractable interactable)
            {
                interactTarget = interactable;

                interactGuideCanvas.enabled = true;
            }
        };

        triggerEventController.OnTriggerExit += (target) =>
        {
            target.OnTriggerExited(gameObject);

            if (target is IInteractable interactable)
            {
                if(interactTarget == interactable)
                {
                    interactTarget = null;
                }

                interactGuideCanvas.enabled = false;
            }
        };
    }

    void InitCollisionController()
    {
        collisionEventController.OnCollisionEnter += (target) => 
        {
            target.OnCollisionEntered(gameObject);
        }; 
        collisionEventController.OnCollisionExit += (target) => 
        {
            target.OnCollisionExited(gameObject);
        }; 
    }


    void InitInputController()
    {
        inputController.Init(playerInput);
        
        inputController.OnMoveEvent += (moveKey) => movementController.SetMoveDir(moveKey.normalized);

        inputController.OnJumpEvent += movementController.Jump;

        inputController.OnInteractEvent += () =>
        {
            if (interactTarget != null)
            {
                interactTarget?.Interact(gameObject);

                interactGuideCanvas.enabled = false;
            }
        };
    }

}

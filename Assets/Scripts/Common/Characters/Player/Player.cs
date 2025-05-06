using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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

    SaveManager saveManager;

    IInteractable interactTarget;


    void FixedUpdate()
    {
        movementController.Move(data.statData.moveSpeed);
    }

    public void Init(PlayerData playerData)
    {
        saveManager = SaveManager.Instance;

        data = playerData;

        ridingController.Init(animator, rendererController);

        movementController.Init(rigid, animator, rendererController);

        InitAppearacneController();

        InitEquipmentController();

        InitTriggerController();

        InitCollisionController();

        InitInputController();

        DontDestroyOnLoad(gameObject);
    }


    void InitAppearacneController()
    {
        appearanceController.OnToggleAppearanceEvent += (appearanceData) =>
        {
            rendererController.ChangeLibraryAsset(appearanceData.Type.ToString(), appearanceData.LibraryAsset);

            saveManager.SaveData.appearanceDataName = appearanceData.DataName;

            saveManager.Save();
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


            var saveEquippedDatas = saveManager.SaveData.equippedDataNames;

            var targetName = saveEquippedDatas.Find(name => name == targetData.ItemName);

            if (targetName != null)
            {
                targetName = targetData.ItemName;
            }
            else
            {
                saveEquippedDatas.Add(targetData.ItemName);
            }

            saveManager.Save();
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

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.XInput;


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

    [SerializeField] PlayerInput playerInput;

    [SerializeField] PlayerMovementController movementController;

    [SerializeField] PlayerRidingController ridingController;

    [SerializeField] ObjectSpriteRendererController rendererController;

    [SerializeField] AppearanceController appearanceController;

    [SerializeField] EquipmentController equipmentController;

    [SerializeField] InputController inputController;

    [SerializeField] InteractTriggerController triggerController;


    PlayerData data;

    DataManager dataManager;


    void FixedUpdate()
    {
        movementController.Move(data.statData.moveSpeed);
    }


    public void Init()
    {
        dataManager = DataManager.Instance;

        data = dataManager.PlayerData;


        ridingController.Init(animator, rendererController);

        movementController.Init(rigid, animator, rendererController);

        InitAppearacneController();

        InitEquipmentController();

        InitTriggerController();

        InitInputController();

        DontDestroyOnLoad(gameObject);
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
        triggerController.Init(gameObject);

        triggerController.OnTriggerEnter += () => interactGuideCanvas.enabled = true;

        triggerController.OnTriggerEixt += () => interactGuideCanvas.enabled = false;
    }


    void InitInputController()
    {
        inputController.Init(playerInput);
        
        inputController.OnMoveEvent += (moveKey) => movementController.SetMoveDir(moveKey.normalized);

        inputController.OnJumpEvent += movementController.Jump;

        inputController.OnInteractEvent += () =>
        {
            if (triggerController.InteractTarget != null)
            {
                triggerController.InteractTarget?.Interact(gameObject);
                interactGuideCanvas.enabled = false;
            }
        };
    }

}

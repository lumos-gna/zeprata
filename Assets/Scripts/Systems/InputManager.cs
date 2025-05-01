using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;


[RequireComponent(typeof(PlayerInput))]
public class InputManager : Singleton<InputManager>
{
    PlayerInput input;

    GameEnum.InputType previousInputType;
    GameEnum.InputType currentInputType;

    public UnityAction OnUIActiveEvent { private get; set; }
    public UnityAction OnUIDisableEvent { private get; set; }
    public UnityAction<Vector2> OnUIScrollEvent { private get; set; }

    public UnityAction OnTownJumpEvent { private get; set; }
    public UnityAction<Vector2> OnTownMoveEvent { private get; set; }
    public UnityAction OnTownInteractEvent { private get; set; }
    public UnityAction OnTapRunnerJumpEvent { private get; set; }

    protected override void Awake()
    {
        base.Awake();

        input = GetComponent<PlayerInput>();
    }

    public void SwitchInputType(GameEnum.InputType inputType)
    {
        if(inputType != currentInputType)
        {
            previousInputType = currentInputType;

            currentInputType = inputType;
        }

        input.SwitchCurrentActionMap(currentInputType.ToString());
    }

    public void SwitchPreviousInputType()
    {
        GameEnum.InputType tempCurrentType = currentInputType;

        currentInputType = previousInputType;

        previousInputType = tempCurrentType;

        input.SwitchCurrentActionMap(currentInputType.ToString());
    }


    void OnUIActive() => OnUIActiveEvent?.Invoke();
    void OnUIDisable() => OnUIDisableEvent?.Invoke();
    void OnUIScroll(InputValue value) => OnUIScrollEvent?.Invoke(value.Get<Vector2>());

    void OnTownMove(InputValue value) => OnTownMoveEvent?.Invoke(value.Get<Vector2>());
    void OnTownInteract() => OnTownInteractEvent?.Invoke();
    void OnTownJump() => OnTownJumpEvent?.Invoke();
    void OnTapRunnerJump() => OnTapRunnerJumpEvent?.Invoke();
}

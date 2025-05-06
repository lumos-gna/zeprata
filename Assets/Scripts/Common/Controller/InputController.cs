using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;


public class InputController : MonoBehaviour
{
    public event UnityAction<bool> OnUIToggleEvent;
    public event UnityAction<Vector2> OnMoveEvent;
    public event UnityAction OnJumpEvent;
    public event UnityAction OnInteractEvent;

    public event UnityAction OnTapRunnerTapEvent;

    GameEnum.InputType previousInputType;
    GameEnum.InputType currentInputType;

    PlayerInput input;
  

    public void Init(PlayerInput input) => this.input = input;

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


    void OnUIEnable() => OnUIToggleEvent?.Invoke(true);
    void OnUIDisable() => OnUIToggleEvent?.Invoke(false);

    void OnMove(InputValue value) => OnMoveEvent?.Invoke(value.Get<Vector2>());
    void OnInteract() => OnInteractEvent?.Invoke();
    void OnJump() => OnJumpEvent?.Invoke();

    void OnTapRunnerTap() => OnTapRunnerTapEvent?.Invoke();

}

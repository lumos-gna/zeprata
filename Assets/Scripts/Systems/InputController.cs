using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;


public class InputController : MonoBehaviour
{
    public event UnityAction<bool> OnUIToggleEvent;
    public UnityAction<Vector2> OnUIScrollEvent { private get; set; }


    public event UnityAction<Vector2> OnMoveEvent;
    public event UnityAction OnJumpEvent;
    public event UnityAction OnInteractEvent;



    public UnityAction OnTapRunnerJumpEvent { private get; set; }


    [SerializeField] PlayerInput input;


    GameEnum.InputType previousInputType;
    GameEnum.InputType currentInputType;


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
    void OnUIScroll(InputValue value) => OnUIScrollEvent?.Invoke(value.Get<Vector2>());

    void OnMove(InputValue value) => OnMoveEvent?.Invoke(value.Get<Vector2>());
    void OnInteract() => OnInteractEvent?.Invoke();
    void OnJump() => OnJumpEvent?.Invoke();

    void OnTapRunnerJump() => OnTapRunnerJumpEvent?.Invoke();

}

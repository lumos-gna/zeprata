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


    public UnityAction<Vector2> OnMoveEvent { private get; set; }
    public UnityAction OnJumpEvent { private get; set; }
    public UnityAction OnInteractEvent { private get; set; }

    public event UnityAction OnMainDisableUIEvent;


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

    void OnMainMove(InputValue value) => OnMoveEvent?.Invoke(value.Get<Vector2>());
    void OnMainInteract() => OnInteractEvent?.Invoke();
    void OnMainJump() => OnJumpEvent?.Invoke();
    void OnMainDisableUI() => OnMainDisableUIEvent?.Invoke();

    void OnTapRunnerJump() => OnTapRunnerJumpEvent?.Invoke();

}

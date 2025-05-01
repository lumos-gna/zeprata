using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;


[RequireComponent(typeof(PlayerInput))]
public class InputManager : Singleton<InputManager>
{
    PlayerInput input;

    public UnityAction OnPlayDialogueEvent { private get; set; }
    public UnityAction OnFinishDialogueEvent { private get; set; }
    public UnityAction OnJumpEvent { private get; set; }
    public UnityAction<Vector2> OnMoveEvent { private get; set; }
    public UnityAction OnInteractEvent { private get; set; }
    public UnityAction OnTapRunnerTapEvent { private get; set; }

    protected override void Awake()
    {
        base.Awake();

        input = GetComponent<PlayerInput>();
    }

    public void SwitchInputType(GameEnum.InputType inputType) => input.SwitchCurrentActionMap(inputType.ToString());
    void OnPlayDialogue() => OnPlayDialogueEvent?.Invoke();
    void OnFinishDialogue() => OnFinishDialogueEvent?.Invoke();
    void OnMove(InputValue value) => OnMoveEvent?.Invoke(value.Get<Vector2>());
    void OnInteract() => OnInteractEvent?.Invoke();
    void OnJump() => OnJumpEvent?.Invoke();
    void OnTap() => OnTapRunnerTapEvent?.Invoke();
}

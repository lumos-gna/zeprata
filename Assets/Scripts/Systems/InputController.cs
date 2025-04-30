using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;


[RequireComponent(typeof(PlayerInput))]
public class InputManager : Singleton<InputManager>
{
    PlayerInput input;

    public event UnityAction OnPlayDialogueEvent;
    public event UnityAction OnFinishDialogueEvent;
    public event UnityAction OnJumpEvent;
    public event UnityAction<Vector2> OnMoveEvent;
    public event UnityAction OnInteractEvent;

    void Awake()
    {
        input = GetComponent<PlayerInput>();

        DontDestroyOnLoad(gameObject);
    }

    public void SwitchInputType(GameEnum.InputType inputType) => input.SwitchCurrentActionMap(inputType.ToString());
    void OnPlayDialogue() => OnPlayDialogueEvent?.Invoke();
    void OnFinishDialogue() => OnFinishDialogueEvent?.Invoke();
    void OnMove(InputValue value) => OnMoveEvent?.Invoke(value.Get<Vector2>());
    void OnInteract() => OnInteractEvent?.Invoke();
    void OnJump() => OnJumpEvent?.Invoke();
}

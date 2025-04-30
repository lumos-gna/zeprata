using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{
    Player player;
    PlayerInput input;


    Vector2 moveKey;
    public Vector2 MoveKey => moveKey;

    private void Awake()
    {
        input = GetComponent<PlayerInput>();
        player = GetComponent<Player>();
    }
  

    public void SwitchType(GameEnum.InputType inputType) => input.SwitchCurrentActionMap(inputType.ToString());

    
    void OnMove(InputValue value) => moveKey = value.Get<Vector2>().normalized;
    void OnInteract() => player.StartInteract();
    void OnJump() => player.Jump();

    void OnNextInteract() => player.NextInteract();
    void OnFinishInteract() => player.FinishInteract();


}

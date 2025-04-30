using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{
    Vector2 moveDir;
    Player player;
    PlayerInput input;
    
    public Vector2 MoveDir => moveDir;

    private void Awake()
    {
        input = GetComponent<PlayerInput>();
        player = GetComponent<Player>();
    }
    
    public void SwitchType(GameEnum.InputType inputType) => input.SwitchCurrentActionMap(inputType.ToString());

    
    void OnMove(InputValue value) => moveDir = value.Get<Vector2>().normalized;
    void OnInteract() =>   player.InteractTarget?.Start(player);
    void OnNextDialogue() =>   player.InteractTarget?.Next(player);
    void OnEndDialogue() =>  player.InteractTarget?.End(player);
}

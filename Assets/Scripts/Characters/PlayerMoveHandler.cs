using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveHandler : MonoBehaviour
{
    [SerializeField] private float moveSpeedDelta;
    
    public void FixedMove(Vector2 inputPos, Rigidbody2D rigidbody)
    {
        Vector2 moveDir = new Vector2(inputPos.x, inputPos.y).normalized;
            
        rigidbody.MovePosition(rigidbody.position +  (moveDir  * moveSpeedDelta * Time.fixedDeltaTime));
    }
}

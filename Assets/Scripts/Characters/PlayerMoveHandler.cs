using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveHandler : MonoBehaviour
{
    Player player;

    [SerializeField] private float moveSpeedDelta;

    private void Awake()
    {
        player = GetComponent<Player>();
    }

    public void FixedMove(Vector2 moveDir, Rigidbody2D rigidbody)
    {
        rigidbody.MovePosition(rigidbody.position +  (moveDir  * moveSpeedDelta * Time.fixedDeltaTime));
    }


}

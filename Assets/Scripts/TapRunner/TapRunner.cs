using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
public class TapRunner : MonoBehaviour
{
    //Animator animator;
    Rigidbody2D rigid;

    [SerializeField] float upForce;

    [SerializeField] TapRunnerController controller;


    private void Awake()
    {
        //animator = GetComponentInChildren<Animator>();
        
        rigid = GetComponent<Rigidbody2D>();

        rigid.gravityScale = 0;
    }


    public void StartPlay()
    {
        rigid.gravityScale = 2;
    }

    public void Jump()
    {
        Vector3 velocity = rigid.velocity;
        velocity.y += upForce;
        rigid.velocity = velocity;
    }



    public void OnCollisionEnter2D(Collision2D collision)
    {
        controller.GameOver();

        Vector3 velocity = rigid.velocity;
        velocity.x += 1;
        rigid.velocity = velocity;

        //animator.SetInteger("IsDie", 1);
    }
}

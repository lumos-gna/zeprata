using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
public class TapRunner : MonoBehaviour
{
    //Animator animator;
    Rigidbody2D rigidbody;

    [SerializeField] float upForce;

    [SerializeField] TapRunnerController controller;


    private void Awake()
    {
        //animator = GetComponentInChildren<Animator>();
        rigidbody = GetComponent<Rigidbody2D>();
    }


    public void ReadyToStart()
    {
        rigidbody.gravityScale = 0;
    }

    void OnStartKey()
    {
        if(!controller.IsStart)
        {
            controller.StartGame();

            rigidbody.gravityScale = 2;
        }
    }

    void OnTapKey()
    {
        if (controller.IsStart)
        {
            Vector3 velocity = rigidbody.velocity;
            velocity.y += upForce;
            rigidbody.velocity = velocity;
        }
    }

    void OnGameEndKey()
    {
        if (controller.IsEndable)
        {
            controller.EndGame();
        }
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        controller.GameOver();

        Vector3 velocity = rigidbody.velocity;
        velocity.x += 1;
        rigidbody.velocity = velocity;

        //animator.SetInteger("IsDie", 1);
    }
}

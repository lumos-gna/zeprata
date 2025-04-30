using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Animator))]
public class PlayerSpriteHandler : MonoBehaviour
{
    bool isJumping = false;

    SpriteRenderer spriteRenderer;
    Animator animator;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    public void Move(bool state) => animator.SetBool("isMove", state);

    public void Flip(bool state) => spriteRenderer.flipX = state;

    public void Jump()
    {
        if (isJumping) return;

        animator.SetBool("isJump", true);

        StartCoroutine(FinishJump());
    }

    IEnumerator FinishJump()
    {
        yield return new WaitForSeconds(0.33f);
        animator.SetBool("isJump", false);
    }



}

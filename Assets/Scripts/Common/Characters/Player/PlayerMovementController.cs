using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementController : MonoBehaviour
{
    Rigidbody2D rigid;
    Animator animator;
    ObjectSpriteRendererController rendererController;

    bool isJumping;

    Vector2 moveDir;

    public void Init(Rigidbody2D rigid, Animator animator, ObjectSpriteRendererController rendererController)
    {
        this.rigid = rigid;
        this.animator = animator;
        this.rendererController = rendererController;
    }

    public void SetMoveDir(Vector2 moveDir) => this.moveDir = moveDir;
 
    public void Move(float speedData)
    {
        float moveSpeed = moveDir != Vector2.zero ? speedData : 0;

        animator.SetFloat("Speed", moveSpeed);

        if(moveSpeed > 0)
        {
            rigid.MovePosition(rigid.position + (moveDir * moveSpeed * Time.fixedDeltaTime));

            if(moveDir.x != 0)
            {
                rendererController.SetRenderersFlipX(moveDir.x < 0);
            }
        }
    }


    public void Jump()
    {
        if (!isJumping)
        {
            isJumping = true;

            animator.SetBool("isJump", isJumping);

            StartCoroutine(FinishJump());
        }
    }

    IEnumerator FinishJump()
    {
        yield return new WaitForSeconds(0.33f);

        isJumping = false;
        animator.SetBool("isJump", isJumping);
    }
}

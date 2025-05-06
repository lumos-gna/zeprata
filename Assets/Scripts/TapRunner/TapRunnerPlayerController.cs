using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TapRunnerPlayerController : MonoBehaviour
{
    [SerializeField] Vector2 jumpForce;
    [SerializeField] Vector2 collisionForce;

    [SerializeField] Transform startPoint;

    TapRunnerController parentController;

    Player player;

    bool isAlive;

    public void Init(TapRunnerController parentController)
    {
        this.parentController = parentController;

        player = FindAnyObjectByType<Player>();

        player.transform.position = startPoint.position;

        var ridingItem = player.EquipmentController.EquippedDatas[GameEnum.ItemType.Riding];

        if (ridingItem != null)
        {
            player.EquipmentController.UnEquip(ridingItem);
        }

        player.RendererController.SetRenderersFlipX(false);

        player.CollisionEventController.OnCollisionEnter += OnCollisionEvent;
        player.InputController.OnTapRunnerTapEvent += OnTap;

        StartCoroutine(InputBlockDelay());
    }
    
    public void StartRunner()
    {
        player.Rigid.gravityScale = 2;

        player.Rigid.freezeRotation = false;

        isAlive = true;
    }

    public void EndRunner()
    {
        player.transform.localRotation = Quaternion.identity;

        player.transform.position = player.Data.townPos;

        player.Rigid.gravityScale = 0;

        player.Rigid.velocity = Vector3.zero;

        player.Rigid.freezeRotation = true;

        player.InputController.OnTapRunnerTapEvent -= OnTap;
        player.CollisionEventController.OnCollisionEnter -= OnCollisionEvent;
    }

 
    void OnTap()
    {
        switch (parentController.gameState)
        {
            case TapRunnerController.GameState.Start:
                parentController.StartGame(); 
                break;

            case TapRunnerController.GameState.Play:
                player.Animator.SetTrigger("Jump");
                player.Rigid.AddForce(jumpForce, ForceMode2D.Impulse);
                break;

            case TapRunnerController.GameState.End:
                parentController.EndGame();
                break;
        }
    }


    void OnCollisionEvent(ICollisionEventable target)
    {
        if(isAlive)
        {
            parentController.GameOver();

            player.Rigid.AddForce(collisionForce, ForceMode2D.Impulse);

            StartCoroutine(InputBlockDelay());

            isAlive = false;
        }
    }


    IEnumerator InputBlockDelay()
    {
        player.InputController.SwitchInputType(GameEnum.InputType.None);

        yield return new WaitForSeconds(0.33f);

        player.InputController.SwitchInputType(GameEnum.InputType.TapRunner);
    }
}

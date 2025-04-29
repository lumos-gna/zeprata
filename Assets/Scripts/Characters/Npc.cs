using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(CapsuleCollider2D))]
public class Npc : MonoBehaviour, ITriggerable<Player>
{
    SpriteRenderer spriteRenderer;

    [SerializeField] private GameObject activeSign;
    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        activeSign.SetActive(false);
    }

    public void OnEnter(Player target)
    {
        activeSign.SetActive(true);
    }

    public void OnExit(Player target)
    {
        activeSign.SetActive(false);
    }
    public void OnStay(Player target){}
}

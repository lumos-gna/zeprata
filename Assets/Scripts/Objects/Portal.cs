using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour, ITriggerable
{
    [SerializeField] private Vector3 targetPos;
    
    public void OnEnter(Player player)
    {
        player.transform.position = targetPos;
    }
    
    public void OnExit(Player player){}
   

    public void OnStay(Player player){}
}

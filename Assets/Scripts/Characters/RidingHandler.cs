using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RidingHandler : MonoBehaviour
{
    [SerializeField] Animator animator;

    PlayerData playerData;
    EquipmentManager equipmentManager;

    private void Awake()
    {
        equipmentManager = EquipmentManager.Instance;
        playerData = DataManager.Instance.PlayerData;
    }

    private void OnEnable()
    {
    }


    void Ride()
    {
        animator.SetBool("isRiding", true);
    }

    void Dismount()
    {
        animator.SetBool("isRiding", false);
    }

}

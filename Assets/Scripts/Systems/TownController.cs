using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TownController : MonoBehaviour
{
    CameraManager cameraManager;


    [SerializeField] float camSpeed;

    [SerializeField] Bounds camClampBounds;

    [SerializeField] TownPlayer townPlayer;
    public TownPlayer TownPlayer => townPlayer;


    private void Awake()
    {
        cameraManager = CameraManager.Instance;
    }


    private void Start()
    {
        townPlayer.Init();

        InputManager.Instance.SwitchInputType(GameEnum.InputType.Town);
    }



    private void LateUpdate()
    {
        cameraManager.LerpMoveCamera(townPlayer.transform.position, camClampBounds, camSpeed);
    }
}

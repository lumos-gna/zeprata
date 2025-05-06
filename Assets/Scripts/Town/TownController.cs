using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TownController : MonoBehaviour
{
    [SerializeField] float camSpeed;

    [SerializeField] Bounds camClampBounds;

    [Space(20f)]
    [SerializeField] TownUIController uiController;

    [SerializeField] TownCameraController cameraController;

    [SerializeField] AppearanceDataTable appearanceDataTable;

    Player player;

    private void Awake()
    {
        player = FindAnyObjectByType<Player>();
        player.InputController.SwitchInputType(GameEnum.InputType.Player);

        uiController.Init(player);
    }


    private void LateUpdate()
    {
        cameraController.LerpMoveCamera(player.transform.position, camClampBounds, camSpeed);
    }
}

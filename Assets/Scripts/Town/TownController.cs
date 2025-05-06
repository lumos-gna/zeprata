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


    [SerializeField] Player playerPrefab;
    [SerializeField] AppearanceDataTable appearanceDataTable;

    Player player;

    private void Awake()
    {
        player = FindAnyObjectByType<Player>();

        if (player == null)
        {
            player = Instantiate(playerPrefab);

            if (player.Data.appearanceData == null)
            {
                var randAppearance = appearanceDataTable.Datas[Random.Range(0, appearanceDataTable.Datas.Length)];

                player.Data.appearanceData = randAppearance;

                player.AppearanceController.ToggleAppearance(randAppearance);
            }
        }


        player.InputController.SwitchInputType(GameEnum.InputType.Player);

        uiController.Init(player);
    }



    private void LateUpdate()
    {
        cameraController.LerpMoveCamera(player.transform.position, camClampBounds, camSpeed);
    }
}

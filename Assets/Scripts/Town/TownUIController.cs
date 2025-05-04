using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TownUIController : MonoBehaviour
{
    [Header("HUD")]
    [SerializeField] Canvas canvasHUD;
    [SerializeField] Button appearanceUIButton;
    [SerializeField] Button ridingUIButton;

    [Space(10f)]
    [Header("Popup")]
    [SerializeField] Canvas canvasPopup;
    [SerializeField] AppearanceUI appearanceUI;
    [SerializeField] StoreUI storeUI;

    Stack<IPopupUI> popupUIStack = new();

    Player player;


    void OnEnable()
        => player.InputController.OnUIToggleEvent += (enabled) => DisablePopup();

    void OnDisable()
        => player.InputController.OnUIToggleEvent -= (enabled) => DisablePopup();


    public void Init(Player player)
    {
        this.player = player;


        appearanceUI.Init(player.AppearanceController);

        storeUI.Init();

        appearanceUIButton.onClick.AddListener(() =>
        {
            popupUIStack.Push(appearanceUI);
            appearanceUI.Enable();
        });

        ridingUIButton.onClick.AddListener(() =>
        {
            storeUI.InitToSlots(GameEnum.ItemType.Riding);
            storeUI.gameObject.SetActive(true);
        });
    }


    void DisablePopup() => popupUIStack.Pop().Disable();

}

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
    [SerializeField] Image appearanceUIIcon;
    [SerializeField] Button ridingUIButton;

    [Space(10f)]
    [Header("Popup")]
    [SerializeField] Canvas canvasPopup;
    [SerializeField] AppearanceUI appearanceUI;
    [SerializeField] StoreUI storeUI;

    Stack<IPopupUI> popupUIStack = new();

    Player player;


    void OnEnable()
    {
        player.InputController.OnUIToggleEvent += (enabled) => DisablePopup();
        player.AppearanceController.OnToggleAppearanceEvent += (data) =>
            appearanceUIIcon.sprite = data.IconSprite;
    }

    void OnDisable()
    {
        player.InputController.OnUIToggleEvent -= (enabled) => DisablePopup();
        player.AppearanceController.OnToggleAppearanceEvent -= (data) =>
          appearanceUIIcon.sprite = data.IconSprite;
    }


    public void Init(Player player)
    {
        this.player = player;


        appearanceUI.Init(this, player.AppearanceController);

        storeUI.Init(this, player);

        appearanceUIIcon.sprite = player.AppearanceController.EquipData.IconSprite;


        appearanceUIButton.onClick.AddListener(() => EnablePopup(appearanceUI));

        ridingUIButton.onClick.AddListener(() =>
        {
            EnablePopup(storeUI);

            storeUI.SetUIState(GameEnum.ItemType.Riding);
        });
    }


    public void DisablePopup()
    {
        popupUIStack.Pop().Disable();

        if(popupUIStack.Count == 0 ) 
        {
            player.InputController.SwitchInputType(GameEnum.InputType.Player);
        }
    }

    public void EnablePopup(IPopupUI popupUI)
    {
        popupUIStack.Push(popupUI);

        popupUI.Enable();

        player.InputController.SwitchInputType(GameEnum.InputType.UI);
    }
}

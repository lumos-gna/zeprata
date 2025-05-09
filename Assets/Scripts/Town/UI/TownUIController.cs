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
    [SerializeField] TownAppearanceUI appearanceUI;
    [SerializeField] TownStoreUI storeUI;
    [SerializeField] TownGameUI gameUI;
    [SerializeField] DialogueUI dialogueUI;

    Stack<IPopupUI> popupUIStack = new();

    Player player;


    void OnEnable()
    {
        player.InputController.OnUIDisableEvent += DisablePopup;

        player.InputController.OnUIEnableEvent += PlayerDialogue;

        player.AppearanceController.OnToggleAppearanceEvent += ChangeUIIcon;
    }

    void OnDisable()
    {
        player.InputController.OnUIDisableEvent -= DisablePopup;

        player.InputController.OnUIEnableEvent -= PlayerDialogue;

        player.AppearanceController.OnToggleAppearanceEvent -= ChangeUIIcon;
    }

    void ChangeUIIcon(AppearanceData data) => appearanceUIIcon.sprite = data.IconSprite;

    public void Init(Player player)
    {
        this.player = player;


        appearanceUI.Init(this, player.AppearanceController);

        storeUI.Init(this, player);

        gameUI.Init(this);

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

    public void ShowDialogue(DialogueData dialogueData)
    {
        dialogueUI.InitDialogue(dialogueData);

        EnablePopup(dialogueUI);
    }

    public void ShowGameUI(string titleText, int targetScore, string targetScene)
    {
        gameUI.Show(titleText, targetScore, targetScene);
        EnablePopup(gameUI);
    }

    void PlayerDialogue()
    {
        if(dialogueUI.gameObject.activeSelf)
        {
            dialogueUI.PlayDialogue(out bool isFinish);

            if (isFinish)
            {
                DisablePopup();
            }
        }
    }
}

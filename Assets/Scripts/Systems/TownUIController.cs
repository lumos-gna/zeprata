using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TownUIController : MonoBehaviour
{
    [SerializeField] Button appearanceUIButton;
    [SerializeField] Button ridingUIButton;

    [SerializeField] AppearanceUI appearanceUI;
    [SerializeField] StoreUI storeUI;

    Stack<IPopupUI> popupUIStack = new();

    InputManager inputManager;

    private void Awake()
    {
        inputManager = InputManager.Instance;

        appearanceUI.Init();
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


    private void OnEnable()
    {
        if(inputManager != null) 
        {
            inputManager.OnMainDisableUIEvent += () => DisablePopup();
        }
    }

    private void OnDisable()
    {
        if (inputManager != null)
        {
            inputManager.OnMainDisableUIEvent -= () => DisablePopup();
        }
    }



    void DisablePopup() => popupUIStack.Pop().Disable();

}

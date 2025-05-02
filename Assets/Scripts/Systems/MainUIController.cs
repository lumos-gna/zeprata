using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MainUIController : MonoBehaviour
{
    [SerializeField] Button appearanceUIButton;

    [SerializeField] AppearanceUI appearanceUI;

    Stack<IPopupUI> popupUIStack = new();

    InputManager inputManager;

    private void Awake()
    {
        inputManager = InputManager.Instance;
    }

    private void Start()
    {
        Init();
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


    public void Init()
    {
        appearanceUI.Init();

        appearanceUIButton.onClick.AddListener(() =>
        {
            popupUIStack.Push(appearanceUI);
            appearanceUI.Enable();
        });
    }

    void DisablePopup() => popupUIStack.Pop().Disable();

}

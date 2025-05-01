using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Content;
using UnityEngine;

public class UIManager : Singleton<UIManager>
{
    InputManager inputManager;


    Stack<GameObject> popupUIStack = new();

    [SerializeField] DialogueUI dialogueUIPrefab;
    [SerializeField] ShopUI shopUIPrefab;

    public DialogueUI DialogueUI { get; private set; }
    public ShopUI ShopUI { get; private set; }


    protected override void Awake()
    {
        base.Awake();
        

        inputManager = InputManager.Instance;

        inputManager.OnUIDisableEvent = () => DisablePopup();
    }



    void EnablePopup(GameObject targetObject)
    {
        popupUIStack.Push(targetObject);

        targetObject.SetActive(true);
    }

    public void DisablePopup()
    {
        if (popupUIStack.Count > 0)
        {
            popupUIStack.Pop().SetActive(false);

            if (popupUIStack.Count == 0)
            {
                inputManager.SwitchPreviousInputType();
            }
        }
    }


    public void EnableDialogue(DialogueData dialogueData)
    {
        if (DialogueUI == null)
        {
            DialogueUI = Instantiate(dialogueUIPrefab);
        }

        DialogueUI.InitDialogue(dialogueData);


        EnablePopup(DialogueUI.gameObject);


        inputManager.SwitchInputType(GameEnum.InputType.UI);

        inputManager.OnUIActiveEvent = () => DialogueUI.NextDialogue();
    }


    public void EnableShop()
    {
        if (ShopUI == null)
        {
            ShopUI = Instantiate(shopUIPrefab);
        }

        EnablePopup(ShopUI.gameObject);


        inputManager.SwitchInputType(GameEnum.InputType.UI);
    }
}

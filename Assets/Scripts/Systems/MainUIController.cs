using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MainUIController : MonoBehaviour
{
    [SerializeField] Button appearanceUIButton;

    [SerializeField] AppearanceUI appearanceUI;

    private void Start()
    {
        Init();
    }

    public void Init()
    {
        appearanceUI.Init();

        appearanceUIButton.onClick.AddListener(() => appearanceUI.gameObject.SetActive(true));
    }
}

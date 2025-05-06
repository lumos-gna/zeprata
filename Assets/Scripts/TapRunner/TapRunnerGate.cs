using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem.XR;

public class TapRunnerGate : MonoBehaviour, ITriggerEventable, IInteractable
{
    [SerializeField] SpriteRenderer spriteRenderer;

    [SerializeField] Canvas canvas;

    [SerializeField] Sprite openSprite;
    [SerializeField] Sprite closeSprite;

    TownUIController uiController;


    private void Start()
    {
        uiController = FindAnyObjectByType<TownUIController>();
    }

    public void Interact(GameObject source)
    {
        if(source.TryGetComponent(out Player player))
        {
            uiController.ShowGameUI("TapRunner", GameManager.Instance.TapRunnerScore, "TapRunnerScene");
        }
    }

    public void OnTriggerEntered(GameObject source)
    {
        canvas.enabled = true;

        spriteRenderer.sprite = openSprite;
    }

    public void OnTriggerExited(GameObject source)
    {
        canvas.enabled = false;

        spriteRenderer.sprite = closeSprite;
    }

}

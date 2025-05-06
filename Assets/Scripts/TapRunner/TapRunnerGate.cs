using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TapRunnerGate : MonoBehaviour, ITriggerEventable, IInteractable
{
    [SerializeField] SpriteRenderer spriteRenderer;

    [SerializeField] TextMeshProUGUI scoreText;


    [SerializeField] Sprite openSprite;
    [SerializeField] Sprite closeSprite;


    private void Start()
    {
        scoreText.text = "Best : " + GameManager.Instance.TapRunnerScore.ToString();
    }

    public void Interact(GameObject source)
    {
        SceneLoadManager.Instance.LoadScene("TapRunnerScene",
            () => GameManager.Instance.PlayerData.townPos = source.transform.position);
    }

    public void OnTriggerEntered(GameObject source)
    {
        spriteRenderer.sprite = openSprite;
    }

    public void OnTriggerExited(GameObject source)
    {
        spriteRenderer.sprite = closeSprite;
    }

}

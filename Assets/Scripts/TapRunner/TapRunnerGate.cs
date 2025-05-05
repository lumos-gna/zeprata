using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TapRunnerGate : MonoBehaviour
{
    [SerializeField] SpriteRenderer spriteRenderer;

    [SerializeField] TextMeshProUGUI scoreText;

    [SerializeField] InteractTrigger interactTrigger;


    [SerializeField] Sprite openSprite;
    [SerializeField] Sprite closeSprite;


    private void Start()
    {
        scoreText.text = "Best : " + DataManager.Instance.TapRunnerScore.ToString();

        interactTrigger.OnInteract = (source) => SceneManager.LoadScene("GameTapRunnerScene");
        
        interactTrigger.OnTriggerEnter = (source) => spriteRenderer.sprite = openSprite;

        interactTrigger.OnTriggerEixt = (source) => spriteRenderer.sprite = closeSprite;
    }
}

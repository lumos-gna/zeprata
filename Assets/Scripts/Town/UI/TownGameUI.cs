using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TownGameUI : MonoBehaviour, IPopupUI
{
    [SerializeField] TextMeshProUGUI titleText;
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] Button closeButton;
    [SerializeField] Button startButton;

    string targetScene;


    public void Disable() => gameObject.SetActive(false);

    public void Enable() => gameObject.SetActive(true);
    

    public void Init(TownUIController townUIController)
    {
        closeButton.onClick.AddListener(() => townUIController.DisablePopup(false));

        startButton.onClick.AddListener(() => SceneLoadManager.Instance.LoadScene(targetScene));
    }

    public void Show(string titleText, int targetScore, string targetScene)
    {
        this.titleText.text = titleText;
     
        this.targetScene = targetScene;

        scoreText.text = $"Score : {targetScore}";
    }
}

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogueManager : Singleton<DialogueManager>
{
    int dialogueIndex = 0;

    bool isRunning;


    DialogueData dialogueData;
    InputManager inputManager;


    [SerializeField] Canvas dialogueCanvas;
    [SerializeField] TextMeshProUGUI nameText;
    [SerializeField] TextMeshProUGUI dialogueText;


    public bool IsRunning => isRunning;

    private void Awake()
    {
        dialogueCanvas.enabled = false;

        inputManager = InputManager.Instance;

        inputManager.OnFinishDialogueEvent += () => DisableDialogue();

        inputManager.OnPlayDialogueEvent += () => NextDialogue();

        DontDestroyOnLoad(gameObject);
        DontDestroyOnLoad(dialogueCanvas.gameObject);
    }



    public void EnableDialogue(DialogueData dialogueData)
    {
        isRunning = true;

        this.dialogueData = dialogueData;

        dialogueCanvas.enabled = true;

        nameText.text = dialogueData.dialogueList[dialogueIndex].speakerName;
        dialogueText.text = dialogueData.dialogueList[dialogueIndex].text;
    }

    public void NextDialogue()
    {
        dialogueIndex++;

        if (dialogueIndex == dialogueData.dialogueList.Count)
        {
            DisableDialogue();
            return;
        }

        nameText.text = dialogueData.dialogueList[dialogueIndex].speakerName;
        dialogueText.text = dialogueData.dialogueList[dialogueIndex].text;

    }

    public void DisableDialogue()
    {
        isRunning = false;

        dialogueIndex = 0;

        dialogueData = null;

        dialogueCanvas.enabled = false;

        inputManager.SwitchInputType(GameEnum.InputType.Player);
    }
}

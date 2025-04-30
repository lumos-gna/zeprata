using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogueUI : MonoBehaviour
{
    int dialogueIndex = 0;

    DialogueData currentDialogueData;

    InputManager inputManager;

    [SerializeField] Canvas dialogueCanvas;
    [SerializeField] TextMeshProUGUI nameText;
    [SerializeField] TextMeshProUGUI dialogueText;

    public bool IsRunning => dialogueCanvas.enabled;

    private void Awake()
    {
        inputManager = InputManager.Instance;

        inputManager.OnFinishDialogueEvent += () => DisableDialogue();

        inputManager.OnPlayDialogueEvent += () => NextDialogue();
    }

    public void EnableDialogue(DialogueData dialogueData)
    {
        currentDialogueData = dialogueData;

        dialogueCanvas.enabled = true;

        nameText.text = dialogueData.dialogueList[dialogueIndex].speakerName;
        dialogueText.text = dialogueData.dialogueList[dialogueIndex].text;
    }

    public void NextDialogue()
    {
        dialogueIndex++;

        if (dialogueIndex == currentDialogueData.dialogueList.Count)
        {
            DisableDialogue();
            return;
        }

        nameText.text = currentDialogueData.dialogueList[dialogueIndex].speakerName;
        dialogueText.text = currentDialogueData.dialogueList[dialogueIndex].text;

    }

    public void DisableDialogue()
    {
        dialogueIndex = 0;

        currentDialogueData = null;

        dialogueCanvas.enabled = false;

        inputManager.SwitchInputType(GameEnum.InputType.Player);
    }

}

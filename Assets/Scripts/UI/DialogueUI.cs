using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogueUI : MonoBehaviour
{
    int dialogueIndex = 0;

    DialogueData currentDialogueData;


    [SerializeField] TextMeshProUGUI nameText;
    [SerializeField] TextMeshProUGUI dialogueText;


    public void InitDialogue(DialogueData dialogueData)
    {
        dialogueIndex = 0;

        currentDialogueData = dialogueData;

        nameText.text = dialogueData.DialogueList[dialogueIndex].speakerName;
        dialogueText.text = dialogueData.DialogueList[dialogueIndex].text;
    }

    public void NextDialogue()
    {
        dialogueIndex++;

        if (dialogueIndex == currentDialogueData.DialogueList.Count)
        {
            UIManager.Instance.DisablePopup();

            currentDialogueData.OnFinishDialogue?.Invoke();
            return;
        }

        nameText.text = currentDialogueData.DialogueList[dialogueIndex].speakerName;
        dialogueText.text = currentDialogueData.DialogueList[dialogueIndex].text;
    }
}

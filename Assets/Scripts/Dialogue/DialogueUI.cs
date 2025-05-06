using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogueUI : MonoBehaviour, IPopupUI
{
    [SerializeField] TextMeshProUGUI nameText;
    [SerializeField] TextMeshProUGUI dialogueText;


    int dialogueIndex = 0;

    DialogueData currentDialogueData;



    public void Disable() => gameObject.SetActive(false);

    public void Enable() => gameObject.SetActive(true);


   
    public void InitDialogue(DialogueData dialogueData)
    {
        dialogueIndex = 0;

        currentDialogueData = dialogueData;

        nameText.text = dialogueData.DialogueList[dialogueIndex].speakerName;
        dialogueText.text = dialogueData.DialogueList[dialogueIndex].text;
    }



    public void PlayDialogue(out bool isFinish)
    {
        isFinish = false;

        dialogueIndex++;

        if (dialogueIndex == currentDialogueData.DialogueList.Count)
        {
            isFinish = true;

            return;
        }

        nameText.text = currentDialogueData.DialogueList[dialogueIndex].speakerName;
        dialogueText.text = currentDialogueData.DialogueList[dialogueIndex].text;
    }
}

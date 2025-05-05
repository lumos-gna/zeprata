using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DynamicTextBox : MonoBehaviour
{
    [SerializeField] RectTransform parentBox;
    [SerializeField] TextMeshProUGUI targetTMP;

    Vector2 defalutBoxSize;


    public void UpdateText(string targetText)
    {
        if(defalutBoxSize == Vector2.zero)
        {
            defalutBoxSize = parentBox.sizeDelta;
        }

        targetTMP.text = targetText;

        parentBox.sizeDelta = new Vector2(targetTMP.preferredWidth + defalutBoxSize.x, defalutBoxSize.y);
    }
}

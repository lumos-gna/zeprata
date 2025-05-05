using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AppearanceController : MonoBehaviour
{
    public event UnityAction<AppearanceData> OnToggleAppearanceEvent;
    public AppearanceData EquipData => equipData;


    AppearanceData equipData;


    public void ToggleAppearance(AppearanceData changedData)
    {
        equipData = changedData;

        OnToggleAppearanceEvent?.Invoke(equipData);
    }

}

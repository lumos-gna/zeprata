using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AppearanceController : MonoBehaviour
{
    public event UnityAction<AppearanceData> OnChangeEvent;
    public AppearanceData EquipData => equipData;


    AppearanceData equipData;


    public void Change(AppearanceData changedData)
    {
        equipData = changedData;

        OnChangeEvent?.Invoke(equipData);
    }

}

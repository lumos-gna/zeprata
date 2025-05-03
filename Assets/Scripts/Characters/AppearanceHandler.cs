using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AppearanceHandler : MonoBehaviour
{
    public event UnityAction<AppearanceData> OnChangeEvent;

    public AppearanceData Data => data;

    [SerializeField] AppearanceData data;

    public void Change(AppearanceData changedData)
    {
        data = changedData;

        OnChangeEvent?.Invoke(data);
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AppearanceManager : Singleton<AppearanceManager>
{
    public event UnityAction<AppearanceData> OnChangeAppearance;

    public EquippedAppearanceData EquippedData
    {
        get
        {
            equippedData ??= DataManager.Instance.EquippedAppearanceData;

            return equippedData;
        }

        set
        {
            equippedData = value;
        }
    }

    EquippedAppearanceData equippedData;


    protected override void Awake()
    {
        base.Awake();
    }

    public void ChangeAppearance(AppearanceData targetData)
    {
        EquippedData.dataName = targetData.DataName;

        OnChangeAppearance?.Invoke(targetData);
    }

}

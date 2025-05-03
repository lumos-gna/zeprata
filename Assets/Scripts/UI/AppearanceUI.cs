using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class AppearanceUI : MonoBehaviour, IPopupUI
{

    [SerializeField] GridLayoutGroup layoutGroup;

    [Space(20f)]
    [SerializeField] Image applyBlockImage;
    [SerializeField] Image previewImage;

    [Space(20f)]
    [SerializeField] Button applyButton;
    [SerializeField] Button closeButton;

    [Space(20f)]
    [SerializeField] AppearanceUISlot slotPrefab;
    [SerializeField] RectTransform slotParent;


    AppearanceManager appearanceManager;

    AppearanceUISlot currentSlot;
    AppearanceUISlot previousSlot;
    
    List<AppearanceUISlot> slotList;

    UnityAction<AppearanceUISlot> OnSelectSlotAction;


    public void Enable()
    {
        gameObject.SetActive(true);


        if(TryGetSlot(appearanceManager.EquippedData.dataName, out AppearanceUISlot targetSlot))
        {
            OnSelectSlotAction(targetSlot);
        }
    }

    public void Disable()
    {
        gameObject.SetActive(false);
    }



    public void Init()
    {
        appearanceManager = AppearanceManager.Instance;


        layoutGroup.cellSize = slotPrefab.GetComponent<RectTransform>().sizeDelta;

        applyButton.onClick.AddListener(ApplyData);

        closeButton.onClick.AddListener(() => Disable());

        OnSelectSlotAction = (slot) =>
        {
            SelectSlot(slot);
            slot.InitUI(true);
        };


        CreateSlot();
    }


    bool TryGetSlot(string targetDataName, out AppearanceUISlot slot)
    {
        for (int i = 0; i < slotList.Count; i++)
        {
            if (slotList[i].AppearanceData.DataName == targetDataName)
            {
                slot = slotList[i];

                return true;
            }
        }
        slot = null;

        return false;
    }



    void CreateSlot()
    {
        slotList = new();

        var appearanceDataDict = DataManager.Instance.AppearanceDataDict;

        foreach (var item in appearanceDataDict)
        {
            var createSlot = Instantiate(slotPrefab, slotParent);

            createSlot.Init(item.Value, OnSelectSlotAction);

            slotList.Add(createSlot);
        }
    }



    void SelectSlot(AppearanceUISlot targetSlot)
    {
        ActiveApplyBlock(appearanceManager.EquippedData.dataName == targetSlot.AppearanceData.DataName);

        bool isNewTarget = currentSlot != targetSlot;

        if (isNewTarget)
        {
            if (currentSlot != null)
            {
                previousSlot = currentSlot;

                previousSlot.InitUI(false);
            }

            currentSlot = targetSlot;

            previewImage.sprite = targetSlot.AppearanceData.PreviewSprite;
            previewImage.SetNativeSize();
        }
    }


    void ApplyData()
    {
        if(currentSlot != null)
        {
            appearanceManager.ChangeAppearance(currentSlot.AppearanceData);

            ActiveApplyBlock(true);
        }
    }

    void ActiveApplyBlock(bool isActive) => applyBlockImage.enabled = isActive;

   
}

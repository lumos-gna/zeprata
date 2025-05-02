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



    DataManager dataManager;

    AppearanceUISlot currentSlot;
    AppearanceUISlot previousSlot;
    
    List<AppearanceUISlot> slotList;

    UnityAction<AppearanceUISlot> OnSelectSlotAction;


    public void Enable()
    {
        gameObject.SetActive(true);
    }

    public void Disable()
    {
        gameObject.SetActive(false);
    }



    public void Init()
    {
        layoutGroup.cellSize = slotPrefab.GetComponent<RectTransform>().sizeDelta;

        applyButton.onClick.AddListener(ApplyData);

        closeButton.onClick.AddListener(() => Disable());

        OnSelectSlotAction = (slot) =>
        {
            SelectSlot(slot);
            slot.InitUI(true);
        };


        slotList = new();

        dataManager = DataManager.Instance;

        var appearanceDataDict = dataManager.AppearanceDataDict;

        foreach (var item in appearanceDataDict)
        {
            var createSlot = Instantiate(slotPrefab, slotParent);

            createSlot.Init(item.Value, OnSelectSlotAction);

            slotList.Add(createSlot);
        }

        var targetSlot = slotList.Find(slot => slot.AppearanceData == dataManager.AppearanceData);

        OnSelectSlotAction?.Invoke(targetSlot);
    }



    void SelectSlot(AppearanceUISlot targetSlot)
    {
        ActiveApplyBlock(dataManager.AppearanceData == targetSlot.AppearanceData);

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
            dataManager.SetAppearanceData(currentSlot.AppearanceData);

            ActiveApplyBlock(true);
        }
    }

    void ActiveApplyBlock(bool isActive) => applyBlockImage.enabled = isActive;

   
}

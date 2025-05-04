using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class AppearanceUI : MonoBehaviour, IPopupUI
{
    [SerializeField] AppearanceDataTable appearanceDataTable;

    [Space(20f)]

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


    AppearanceController appearanceController;


    List<AppearanceUISlot> slotList;

    AppearanceUISlot currentSlot;
    AppearanceUISlot previousSlot;


    UnityAction<AppearanceUISlot> OnSelectSlotAction;



    public void Enable()
    {
        gameObject.SetActive(true);


        if(TryGetSlot(appearanceController.EquipData, out AppearanceUISlot targetSlot))
        {
            OnSelectSlotAction(targetSlot);
        }
    }

    public void Disable()
    {
        gameObject.SetActive(false);
    }



    public void Init(AppearanceController appearanceController)
    {
        this.appearanceController = appearanceController;


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


    bool TryGetSlot(AppearanceData targetData, out AppearanceUISlot slot)
    {
        for (int i = 0; i < slotList.Count; i++)
        {
            if (slotList[i].AppearanceData == targetData)
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

        for (int i = 0; i < appearanceDataTable.Datas.Length; i++)
        {
            var createSlot = Instantiate(slotPrefab, slotParent);

            createSlot.Init(appearanceDataTable.Datas[i], OnSelectSlotAction);

            slotList.Add(createSlot);
        }
    }



    void SelectSlot(AppearanceUISlot targetSlot)
    {
        ActiveApplyBlock(appearanceController.EquipData == targetSlot.AppearanceData);

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
            appearanceController.Change(currentSlot.AppearanceData);

            ActiveApplyBlock(true);
        }
    }

    void ActiveApplyBlock(bool isActive) => applyBlockImage.enabled = isActive;

   
}

using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;


public class TownAppearanceUI : MonoBehaviour, IPopupUI
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
    [SerializeField] TownAppearanceUISlot slotPrefab;
    [SerializeField] RectTransform slotParent;


    AppearanceController appearanceController;


    List<TownAppearanceUISlot> slotList;

    TownAppearanceUISlot currentSlot;
    TownAppearanceUISlot previousSlot;


    UnityAction<TownAppearanceUISlot> OnSelectSlotAction;



    public void Enable()
    {
        gameObject.SetActive(true);


        if(TryGetSlot(appearanceController.EquipData, out TownAppearanceUISlot targetSlot))
        {
            OnSelectSlotAction(targetSlot);
        }
    }

    public void Disable()
    {
        gameObject.SetActive(false);
    }



    public void Init(TownUIController townUIController, AppearanceController appearanceController)
    {
        this.appearanceController = appearanceController;


        layoutGroup.cellSize = slotPrefab.GetComponent<RectTransform>().sizeDelta;

        applyButton.onClick.AddListener(ApplyData);

        closeButton.onClick.AddListener(() => townUIController.DisablePopup(false));

        OnSelectSlotAction = (slot) =>
        {
            SelectSlot(slot);
            slot.InitUI(true);
        };

        CreateSlot();
    }


    bool TryGetSlot(AppearanceData targetData, out TownAppearanceUISlot slot)
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



    void SelectSlot(TownAppearanceUISlot targetSlot)
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
            appearanceController.ToggleAppearance(currentSlot.AppearanceData);

            ActiveApplyBlock(true);
        }
    }

    void ActiveApplyBlock(bool isActive) => applyBlockImage.enabled = isActive;

   
}

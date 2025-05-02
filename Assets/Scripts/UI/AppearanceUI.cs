using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class AppearanceUI : MonoBehaviour
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

    UnityAction<AppearanceUISlot> OnSelectSlotAction;


    public void Init()
    {
        layoutGroup.cellSize = slotPrefab.GetComponent<RectTransform>().sizeDelta;

        applyButton.onClick.AddListener(ApplyData);

        closeButton.onClick.AddListener(() => gameObject.SetActive(false));

        OnSelectSlotAction = (slot) =>
        {
            SelectSlot(slot);
            slot.InitUI(true);
        };



        dataManager = DataManager.Instance;

        var appearanceDatas = dataManager.AppearanceDatas;

        for (int i = 0; i < appearanceDatas.Count; i++)
        {
            var createSlot = Instantiate(slotPrefab, slotParent);

            createSlot.Init(appearanceDatas[i], OnSelectSlotAction);

            if (i == 0)
            {
                OnSelectSlotAction?.Invoke(createSlot);
            }
        }
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
        dataManager.AppearanceData = currentSlot?.AppearanceData;

        ActiveApplyBlock(true);
    }

    void ActiveApplyBlock(bool isActive) => applyBlockImage.enabled = isActive;
   
}

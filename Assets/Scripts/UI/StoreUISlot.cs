using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class StoreUISlot : MonoBehaviour
{
    public StoreItemData StoreItemData => storeItemData;

    public Image LockCoverImage => lockCoverImage;
    public Image SelectedImage => selectedImage;


    [SerializeField] Image iconImage;

    [SerializeField] Image selectedImage;

    [SerializeField] Image lockCoverImage;

    [SerializeField] TextMeshProUGUI nameText;

    [SerializeField] Button selectButton;


    StoreItemData storeItemData;

    public void InitToItemData(StoreItemData storeItemData)
    {
        this.storeItemData = storeItemData;

        var itemData = DataManager.Instance.ItemDataDict[storeItemData.itemName];

        iconImage.sprite = itemData.IconSprite;
        iconImage.SetNativeSize();

        nameText.text = itemData.ItemName;

        lockCoverImage.enabled = !storeItemData.isPurchased;
    }

    public void OnCreated(UnityAction<StoreUISlot> onSelectAction)
    {
        selectButton.onClick.AddListener(() => onSelectAction?.Invoke(this));
    }
}

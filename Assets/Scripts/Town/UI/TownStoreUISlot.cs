using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class TownStoreUISlot : MonoBehaviour
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

    public void Init(UnityAction<TownStoreUISlot> onSelectAction)
    {
        selectButton.onClick.AddListener(() => onSelectAction?.Invoke(this));
    }

    public void SetSlot(StoreItemData storeItemData)
    {
        this.storeItemData = storeItemData;

        var itemData = storeItemData.ItemData;

        iconImage.sprite = itemData.IconSprite;
        iconImage.SetNativeSize();

        nameText.text = itemData.ItemName;

        lockCoverImage.enabled = !storeItemData.IsPurchased;
    }
}

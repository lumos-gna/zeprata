using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class StoreUISlot : MonoBehaviour
{
    StoreItemData storeItemData;

    [SerializeField] Image iconImage;

    [SerializeField] Image selectedImage;

    [SerializeField] Image lockCoverImage;

    [SerializeField] TextMeshProUGUI nameText;

    [SerializeField] Button selectButton;

    public StoreItemData StoreItemData => storeItemData;

    public Image LockCoverImage => lockCoverImage;
    public Image SelectedImage => selectedImage;

    public void InitToItemData(StoreItemData storeItemData)
    {
        this.storeItemData = storeItemData;

        var itemData = storeItemData.ItemData;

        iconImage.sprite = itemData.IconSprite;
        iconImage.SetNativeSize();

        nameText.text = itemData.Name;

        lockCoverImage.enabled = !storeItemData.IsPurchased;
    }

    public void OnCreated(UnityAction<StoreUISlot> onSelectAction)
    {
        selectButton.onClick.AddListener(() => onSelectAction?.Invoke(this));
    }
}

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopSpirteAssetSlot : MonoBehaviour
{

    [SerializeField] TextMeshProUGUI priceText;
    [SerializeField] Image iconImage;
    [SerializeField] Button button;


    ShopItemData shopItemData;
    public ShopItemData ShopItemData => shopItemData;

    public void Init(ShopItemData shopItemData)
    {
        this.shopItemData = shopItemData;

        var itmeData = shopItemData.ItemData;

        priceText.text = itmeData.Price.ToString();

        iconImage.sprite = itmeData.IconSprite;

        iconImage.SetNativeSize();
    }

}

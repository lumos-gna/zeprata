using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopItemSlot : MonoBehaviour
{

    [SerializeField] TextMeshProUGUI priceText;
    [SerializeField] Image iconImage;
    [SerializeField] Button button;

    [SerializeField] GameObject soldOutPanelObject;


    public void Init(ShopItemData shopItemData)
    {
        var itmeData = shopItemData.ItemData;

        priceText.text = itmeData.Price.ToString();

        iconImage.sprite = itmeData.IconSprite;

        iconImage.SetNativeSize();


        if(shopItemData.Count == 0)
        {
            SetSoldOut();
        }


        button.onClick.AddListener(() =>
        {
            ShopManager.Instance.TryBuyItem(shopItemData);

            if (shopItemData.Count == 0)
            {
                SetSoldOut();
            }
        });
    }

    void SetSoldOut()
    {
        soldOutPanelObject.SetActive(true);
        priceText.gameObject.SetActive(false);
    }
}

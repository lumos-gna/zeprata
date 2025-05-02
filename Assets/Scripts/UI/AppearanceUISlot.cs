using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class AppearanceUISlot : MonoBehaviour
{
    public AppearanceData AppearanceData => appearanceData;

    [SerializeField] Image iconImage;
    [SerializeField] Image selectedImage;
    [SerializeField] Image blockImage;

    [SerializeField] Button selectButton;


    AppearanceData appearanceData;


    public void Init(AppearanceData appearanceData, UnityAction<AppearanceUISlot> selectCallback)
    {
        this.appearanceData = appearanceData;

        iconImage.sprite = appearanceData.IconSprite;
        iconImage.SetNativeSize();

        selectButton.onClick.AddListener(() => selectCallback?.Invoke(this));
    }

    public void InitUI(bool isSelected)
    {
        selectedImage.enabled = isSelected;
        blockImage.enabled = isSelected;
    }
    
}
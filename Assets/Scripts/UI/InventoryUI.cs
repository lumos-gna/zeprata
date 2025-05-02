using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    Dictionary<GameEnum.ItemType, GameObject> categoryMenus;
    Dictionary<GameEnum.ItemType, Button> categoryButtons;

    [SerializeField] GameObject characterMenu;
    [SerializeField] GameObject ridingMenu;

    [Space(10f)]
    [SerializeField] Button characterMenuButton;
    [SerializeField] Button ridingMenuButton;


    private void Awake()
    {
        categoryMenus = new()
        {
            {GameEnum.ItemType.Character, characterMenu },
            {GameEnum.ItemType.Riding, ridingMenu }
        };

        categoryButtons = new()
        {
            {GameEnum.ItemType.Character, characterMenuButton },
            {GameEnum.ItemType.Riding, ridingMenuButton }
        };

        foreach (var item in categoryButtons)
        {
            item.Value.onClick.AddListener(() => 
            {
                categoryMenus[item.Key].transform.SetAsLastSibling();
            });
        }
    }

}

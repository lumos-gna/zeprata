using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopUI : MonoBehaviour
{
    Vector2 scrollPos;

    [SerializeField] ScrollRect scrollRect;

    private void Start()
    {
        InputManager.Instance.OnUIScrollEvent = (value) => MoveScroll(value);
    }

    public void MoveScroll(Vector2 scrollPos)
    {
        scrollRect.content.anchoredPosition = scrollPos;
    }
}

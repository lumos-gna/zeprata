using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ScreenFadeUI : MonoBehaviour
{
    [SerializeField] Canvas canvas;
    [SerializeField] Image fadeImage;
    [SerializeField] float duration;

    public IEnumerator Fade(bool isFadeIn)
    {
        float startAlpha = isFadeIn ? 1 : 0;
        float targetAlpha = isFadeIn ? 0 : 1;

        float time = 0f;

        Color color = fadeImage.color;


        color.a = startAlpha;

        fadeImage.color = color;

        canvas.enabled = true;

        while (time < duration)
        {
            time += Time.deltaTime;

            float alpha = Mathf.Lerp(startAlpha, targetAlpha, time / duration);

            color.a = alpha;

            fadeImage.color = color;

            yield return null;
        }

        canvas.enabled = !isFadeIn;
    }
}

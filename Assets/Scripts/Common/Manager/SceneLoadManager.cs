using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class SceneLoadManager : MonoBehaviour
{
    public static SceneLoadManager Instance => instance;

    static SceneLoadManager instance;


    [SerializeField] ScreenFadeUI fadeUIPrefab;


    ScreenFadeUI screenFadeUI;


    private void Awake()
    {
        if (instance != null)
        {
            if (instance != this)
            {
                Destroy(gameObject);
                return;
            }
        }
        else
        {
            instance = this;

            DontDestroyOnLoad(gameObject);
        }

        if (screenFadeUI == null)
        {
            screenFadeUI = Instantiate(fadeUIPrefab);

            DontDestroyOnLoad(screenFadeUI);
        }
    }


    public void LoadScene(string sceneName, UnityAction fadeOutCallback = null) => StartCoroutine(LoadSceneCoroutine(sceneName, fadeOutCallback));
    
    IEnumerator LoadSceneCoroutine(string sceneName, UnityAction fadeOutCallback)
    {
        yield return screenFadeUI.Fade(false);

        fadeOutCallback?.Invoke();

        yield return SceneManager.LoadSceneAsync(sceneName);

        yield return screenFadeUI.Fade(true);
    }
}

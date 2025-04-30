using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenePortal : MonoBehaviour, IInteractable
{
    public string sceneName;
    public virtual void Interact(Player player)
    {
        SceneManager.LoadScene(sceneName);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEditor.VersionControl;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.U2D.Animation;

public class ObjectSpriteRendererController : MonoBehaviour
{
    [SerializeField] ObjectSpriteRenderer[] renderers;


    public bool TryGetRenderer(string partsKey, out ObjectSpriteRenderer target)
    {
        for (int i = 0; i < renderers.Length; i++)
        {
            if (renderers[i].Key == partsKey)
            {
                target = renderers[i];

                return true;
            }
        }

        target = null;

        return false;
    }

    public void ChangeLibraryAsset(string partsKey, SpriteLibraryAsset asset)
    {
        if(TryGetRenderer(partsKey, out ObjectSpriteRenderer target)) 
        {
            target.SpriteLibrary.spriteLibraryAsset = asset;
        }
    }

    public void SetRenderersFlipX(bool isFlip)
    {
        for (int i = 0; i < renderers.Length; i++)
        {
            renderers[i].SpriteRenderer.flipX = isFlip;
        }
    }

    public void SetRenderersFlipY(bool isFlip)
    {
        for (int i = 0; i < renderers.Length; i++)
        {
            renderers[i].SpriteRenderer.flipY = isFlip;
        }
    }
}

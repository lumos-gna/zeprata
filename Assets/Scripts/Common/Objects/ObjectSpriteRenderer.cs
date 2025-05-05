using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D.Animation;


public class ObjectSpriteRenderer : MonoBehaviour
{
    public string Key => key;
    public SpriteRenderer SpriteRenderer => spriteRenderer;
    public SpriteLibrary SpriteLibrary => spriteLibrary;


    [SerializeField] string key;

    [Space(10f)]
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] SpriteResolver SpriteResolver;
    [SerializeField] SpriteLibrary spriteLibrary;
}

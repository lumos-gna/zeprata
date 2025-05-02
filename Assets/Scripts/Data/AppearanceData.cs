using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D.Animation;

[CreateAssetMenu(menuName = "ScriptableObjects/AppearanceData")]
public class AppearanceData : ScriptableObject
{
    [SerializeField] string appearanceName;

    [SerializeField] Sprite iconSprite;

    [SerializeField] Sprite previewSprite;

    [SerializeField] SpriteLibraryAsset libraryAsset;


    public string AppearanceName => appearanceName;
    public Sprite IconSprite => iconSprite;
    public Sprite PreviewSprite => previewSprite;
    public SpriteLibraryAsset LibraryAsset => libraryAsset;
}

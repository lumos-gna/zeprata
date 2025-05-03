using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D.Animation;

[CreateAssetMenu(menuName = "ScriptableObjects/AppearanceData")]
public class AppearanceData : ScriptableObject
{
    [SerializeField] GameEnum.AppearanceType type;

    [SerializeField] string dataName;

    [SerializeField] Sprite iconSprite;

    [SerializeField] Sprite previewSprite;

    [SerializeField] SpriteLibraryAsset libraryAsset;


    public GameEnum.AppearanceType Type => type;
    public string DataName => dataName;
    public Sprite IconSprite => iconSprite;
    public Sprite PreviewSprite => previewSprite;
    public SpriteLibraryAsset LibraryAsset => libraryAsset;
}

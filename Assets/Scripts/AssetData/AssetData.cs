using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssetData : MonoBehaviour
{
    public static AssetDataItems Items;
    public static AssetDataIcons Icons;
    public static AssetDataMaterials Materials;

    private void Awake()
    {
        Items = Resources.Load<AssetDataItems>("Items");
        Icons = Resources.Load<AssetDataIcons>("Icons");
        Materials = Resources.Load<AssetDataMaterials>("Materials");
    }
}

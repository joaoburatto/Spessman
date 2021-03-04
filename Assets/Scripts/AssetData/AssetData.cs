using System;
using System.Collections;
using System.Collections.Generic;
using Mono.CecilX;
using UnityEngine;

public class AssetData : MonoBehaviour
{
    public static AssetDataItems Items;
    public static AssetDataIcons Icons;

    private void Awake()
    {
        Items = Resources.Load<AssetDataItems>("Items");
        Icons = Resources.Load<AssetDataIcons>("Icons");
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using Mono.CecilX;
using UnityEngine;

public class AssetData : MonoBehaviour
{
    public static AssetDataItems Items;

    private void Awake()
    {
        Items = Resources.Load<AssetDataItems>("Items");
    }
}

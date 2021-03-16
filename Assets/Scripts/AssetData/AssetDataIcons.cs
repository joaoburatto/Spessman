using System.Collections;
using System.Collections.Generic;
using Spessman.Inventory;
using UnityEngine;

[CreateAssetMenu(fileName = "IconsData", menuName = "AssetData/Icons", order = 1)]
public class AssetDataIcons : ScriptableObject
{
    public Sprite[] icons;
    
    public Sprite GetAsset(string name)
    {
        name = name.ToLower();
        foreach (Sprite icon in icons)
        {
            if (icon.name == name) return icon;
        }

        return null;
    }
}

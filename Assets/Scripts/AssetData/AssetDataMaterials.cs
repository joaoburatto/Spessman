using System.Collections;
using System.Collections.Generic;
using Spessman.Inventory;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemsData", menuName = "AssetData/Materials", order = 3)]
public class AssetDataMaterials : ScriptableObject
{
    public Material[] materials;
    
    public Material GetAsset(string name)
    {
        name = name.ToLower();
        foreach (Material material in materials)
        {
            if (material.name == name) return material;
        }

        return null;
    }
}

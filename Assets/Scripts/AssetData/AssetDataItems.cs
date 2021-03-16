using System.Collections;
using System.Collections.Generic;
using Spessman.Inventory;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemsData", menuName = "AssetData/Items", order = 1)]
public class AssetDataItems : ScriptableObject
{
    public GameObject[] item;
    
    public GameObject GetAsset(string name)
    {
        name = name.ToLower();
        foreach (GameObject item in item)
        {
            if (item.name == name) return item;
        }

        return null;
    }
}

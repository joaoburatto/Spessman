﻿using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Spessman.Inventory.UI
{
    public class ItemGridItem : ItemDisplay
    {
        public override void OnDropAccepted()
        {
            base.OnDropAccepted();
            (InventoryDisplayElement as ItemGrid)?.RemoveGridItem(this);
            Destroy(gameObject);
        }
    }
}
using System;
using Mirror;
using Spessman.Interactions;
using UnityEngine;
using Utilities.QuickOutline.Scripts;

namespace Spessman.Inventory
{
    [RequireComponent(typeof(Outline))]
    [RequireComponent(typeof(NetworkTransform))]
    public class Selectable : InteractionSourceNetworkBehaviour
    {
        public Outline outline;

        protected void Start()
        {
            outline = GetComponent<Outline>();

            if (outline == null)
            {
                outline = gameObject.AddComponent<Outline>();
                outline.OutlineWidth = 6;
            }
        }
    }
}
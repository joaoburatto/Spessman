﻿using Spessman.Inventory;

namespace Spessman.Interactions.Extensions
{
    public static class InteractionEventExtensions
    {
        public static Item GetSourceItem(this InteractionEvent interactionEvent)
        {
            if (interactionEvent.Source is IGameObjectProvider source)
            {
                Item item = source.GameObject.GetComponent<Item>();
                if (item != null)
                {
                    return item;
                }
            }

            return null;
        }
    }
}
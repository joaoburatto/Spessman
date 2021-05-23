using System.Collections;
using System.Collections.Generic;
using Spessman.Interactions;
using Spessman.Interactions.Extensions;
using Spessman.Inventory;
using Spessman.Inventory.Extensions;
using UnityEngine;

public class Soda : Item
{
    private class DrinkInteraction : Interaction
    {
        public override Sprite GetIcon(InteractionEvent interactionEvent)
        {
            return AssetData.Icons.GetAsset("fire");
        }

        public override bool CanInteract(InteractionEvent interactionEvent)
        {
            return true;
        }

        public override bool Start(InteractionEvent interactionEvent, InteractionReference reference)
        {
            if (interactionEvent.Source is Soda soda)
            {
                // lmao
                soda.currentAnimationClip = soda.animationClips[1];
            }
            return true;
        }
    }
    public override IInteraction[] GenerateInteractions(InteractionEvent interactionEvent)
    {
        DrinkInteraction drinkInteraction = new DrinkInteraction();

        IInteraction[] interactions = new IInteraction[] { new PickupInteraction() { icon = sprite }, drinkInteraction };
        
        return interactions;
    }
}

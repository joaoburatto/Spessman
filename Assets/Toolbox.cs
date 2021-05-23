using System.Collections;
using System.Collections.Generic;
using Spessman.Interactions;
using Spessman.Interactions.Extensions;
using Spessman.Inventory;
using Spessman.Inventory.Extensions;
using UnityEngine;

public class Toolbox : Item
{
    private class ContainerInteraction : Interaction
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
            Animator animator = interactionEvent.Target.GetComponent<Animator>();
            animator.SetTrigger("Squish");
            return true;
        }
    }
    public override IInteraction[] GenerateInteractions(InteractionEvent interactionEvent)
    {
        ContainerInteraction containerInteraction = new ContainerInteraction();

        IInteraction[] interactions = new IInteraction[] { new PickupInteraction() { icon = sprite }, containerInteraction };
        
        return interactions;
    }
}

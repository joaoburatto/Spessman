using System.Collections;
using System.Collections.Generic;
using Spessman.Interactions;
using Spessman.Interactions.Extensions;
using Spessman.Inventory;
using Spessman.Inventory.Extensions;
using UnityEngine;

public class Flashlight : Item
{
    public Renderer bulb;
    public Light light;
    
    public Material on;
    public Material off;

    public bool state;

    public override void Start()
    {
        base.Start();

        on = AssetData.Materials.GetAsset("Emission");
        off = AssetData.Materials.GetAsset("Palette");

    }

    public void ChangeState()
    {
        state = !state;

        light.enabled = state;
        bulb.material = state ? on : off;
    }
    
    private class ToggleFlashlight : Interaction
    {
        public override string GetName(InteractionEvent interactionEvent)
        {
            return "toggle power";
        }

        public override Sprite GetIcon(InteractionEvent interactionEvent)
        {
            return AssetData.Icons.GetAsset("power");
        }

        public override bool Start(InteractionEvent interactionEvent,  InteractionReference reference)
        {
            if (interactionEvent.Source is Flashlight flashlight)
            {
              flashlight.ChangeState();
              return true;
            }
            
            if (interactionEvent.Target is Flashlight flashlight1)
            {
                flashlight1.ChangeState();
                return true;
            }
            //Flashlight flashlight = interactionEvent.Source?.GetComponentInTree<Flashlight>();

            return false;
        }
    }
    
    public override void CreateInteractions(IInteractionTarget[] targets, List<InteractionEntry> interactions)
    {
        base.CreateInteractions(targets, interactions);

        ToggleFlashlight toggle = new ToggleFlashlight();
        interactions.Insert(1, new InteractionEntry(null, toggle));
        
    }

    public override IInteraction[] GenerateInteractions(InteractionEvent interactionEvent)
    {
        ToggleFlashlight toggle = new ToggleFlashlight();

        IInteraction[] interactions = new IInteraction[] { new PickupInteraction { icon = sprite }, toggle };
        
        return interactions;
    }
}

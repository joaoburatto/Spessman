using Spessman.Interactions;
using Spessman.Interactions.Extensions;
using System;
using System.Collections;
using UnityEngine;

namespace Spessman.Inventory.Extensions
{
    public class PickupInteraction : DelayedInteraction
    {
        public Sprite icon;
        
        public IClientInteraction CreateClient(InteractionEvent interactionEvent)
        {
            return null;
        }

        public override string GetName(InteractionEvent interactionEvent)
        {
            return "Pick up";
        }

        public override Sprite GetIcon(InteractionEvent interactionEvent)
        {
            return AssetData.Icons.GetAsset("pickup");
        }

        public override bool CanInteract(InteractionEvent interactionEvent)
        {
            if (interactionEvent.Target is IGameObjectProvider targetBehaviour && interactionEvent.Source is Hands hands)
            {
                if (!hands.SelectedHandEmpty)
                {
                    return false;
                }
                
                Item item = targetBehaviour.GameObject.GetComponent<Item>();
                if (item == null)
                {
                    return false;
                }

                return InteractionExtensions.RangeCheck(interactionEvent) && !item.InContainer();
            }

            return false;
        }

        public override bool Start(InteractionEvent interactionEvent, InteractionReference reference)
        {
            if (interactionEvent.Source is Hands hands && interactionEvent.Target is Item target)
            {
                Delay = .4f;
                hands.IKManager.PickupAnimationHelper(target.transform.position, Delay);
                startTime = Time.time;
                lastCheck = startTime;
                
                return true;
            }
            return false;
        }
        
        public override void Cancel(InteractionEvent interactionEvent, InteractionReference reference)
        {
            
        }

        protected override void StartDelayed(InteractionEvent interactionEvent)
        {
            if (interactionEvent.Source is Hands hands && interactionEvent.Target is Item target)
            {
                hands.IKManager.UnlockHandIK();
                hands.Pickup(target);
            }
        }
    }
}
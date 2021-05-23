using System.Collections;
using System.Collections.Generic;
using Mirror;
using Spessman.Interactions;
using Spessman.Interactions.Extensions;
using Spessman.Inventory;
using UnityEngine;

public class Gun : Item
{
    public Transform projectilePoint;
    public GameObject projectile;

    public ParticleSystem particleSystem;
    
    private class GunFireInteraction : Interaction
    {
        public override Sprite GetIcon(InteractionEvent interactionEvent)
        {
            return AssetData.Icons.GetAsset("fire");
        }

        public override bool CanInteract(InteractionEvent interactionEvent)
        {
            return true;
        }

        public override bool Start(InteractionEvent interactionEvent,  InteractionReference reference)
        {
            Debug.Log("fire");
            Gun gun = interactionEvent.Source?.GetComponentInTree<Gun>();
            GameObject projectileInstance =
                Instantiate(gun.projectile, gun.projectilePoint.position, gun.projectilePoint.rotation);
            
            gun.particleSystem.Play();
            
            NetworkServer.Spawn(projectileInstance);
            
            return true;
        }
    }

    public override void CreateInteractions(IInteractionTarget[] targets, List<InteractionEntry> interactions)
    {
        base.CreateInteractions(targets, interactions);

        GunFireInteraction fire = new GunFireInteraction();
        interactions.Insert(0, new InteractionEntry(null, fire));
        
    }
}

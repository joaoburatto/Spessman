using Mirror;
using Spessman;
using Spessman.Entities;
using UnityEngine;

namespace Spessman.Inventory.UI
{
    public class ClothingUi : MonoBehaviour
    {
        public void Start()
        {
            if (NetworkServer.active && !NetworkClient.active)
            {
                Destroy(this);
                return;
            }
            
            // Connects ui clothing slots to containers on the creature
            var inventory = transform.GetComponentInParent<InventoryUi>().Inventory;
            GameObject entity = inventory.Hands.GetComponentInParent<Entity>().gameObject;
            var clothingContainers = entity.GetComponent<ClothingContainers>();
            var slots = GetComponentsInChildren<SingleItemContainerSlot>();
            foreach (SingleItemContainerSlot slot in slots)
            {
                if (clothingContainers.Containers.TryGetValue(slot.name, out AttachedContainer container))
                {
                    slot.Inventory = inventory;
                    slot.Container = container;
                }
            }
        }
    }
}
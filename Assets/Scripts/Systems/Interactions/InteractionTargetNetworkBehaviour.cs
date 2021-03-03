using Mirror;
using UnityEngine;

namespace Spessman.Interactions
{
    public abstract class InteractionTargetNetworkBehaviour : NetworkBehaviour, IInteractionTarget, IGameObjectProvider
    {
        public abstract IInteraction[] GenerateInteractions(InteractionEvent interactionEvent);
        public GameObject GameObject => gameObject;
    }
}
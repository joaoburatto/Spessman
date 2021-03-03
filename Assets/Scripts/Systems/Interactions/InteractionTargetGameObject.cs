using UnityEngine;

namespace Spessman.Interactions
{
    /// <summary>
    /// Interaction target for target game objects without their own interaction target 
    /// </summary>
    public class InteractionTargetGameObject : IInteractionTarget, IGameObjectProvider
    {
        public InteractionTargetGameObject(GameObject gameObject)
        {
            GameObject = gameObject;
        }

        public GameObject GameObject { get; }
        
        public IInteraction[] GenerateInteractions(InteractionEvent interactionEvent)
        {
            return new IInteraction[0];
        }
    }
}
using Spessman.Entities;
using UnityEngine;

namespace Spessman.Interactions
{
    public class InteractionEvent
    {
        /// <summary>
        /// The owner of that interaction, who triggered it.
        /// Can be null if the item interacted with itself.
        /// </summary>
        public Entity Owner;
        /// <summary>
        /// The source which caused the interaction
        /// </summary>
        public IInteractionSource Source { get; }
        /// <summary>
        /// The target of the interaction, can be null
        /// </summary>
        public IInteractionTarget Target { get; set; }
        /// <summary>
        /// The point at which the interaction took place
        /// </summary>
        public Vector3 Point { get; }

        public InteractionEvent(Entity owner, IInteractionSource source, IInteractionTarget target, Vector3 point = new Vector3())
        {
            Owner = owner;
            Source = source;
            Target = target;
            Point = point;
        }
    }
}
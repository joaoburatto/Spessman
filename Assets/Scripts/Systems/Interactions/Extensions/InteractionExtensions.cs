using UnityEngine;

namespace Spessman.Interactions.Extensions
{
    public static class InteractionExtensions
    {
        public static bool RangeCheck(InteractionEvent interactionEvent)
        {
            Vector3 point = interactionEvent.Point;

            // Ignore range when there is no point
            if (point.sqrMagnitude < 0.001)
            {
                return true;
            }

            var interactionRangeLimit = interactionEvent.Source.GetComponentInTree<IInteractionRangeLimit>(out IGameObjectProvider provider);
            if (interactionRangeLimit == null)
            {
                // No range limit
                return true;
            }

            Vector3 sourcePosition;
            if (provider is IInteractionOriginProvider origin)
            {
                // Object has a custom interaction origin
                sourcePosition = origin.InteractionOrigin;
            }
            else
            {
                // Use default game object origin
                sourcePosition = provider.GameObject.transform.position;
            }

            RangeLimit range = interactionEvent.Source.GetRange();
            if (range.IsInRange(sourcePosition, point)) 
            {
                return true;
            }

            Collider targetCollider = interactionEvent.Target.GetComponent<Collider>();
            if (targetCollider != null)
            {
                Vector3 closestPointOnCollider = targetCollider.ClosestPointOnBounds(sourcePosition);
                return range.IsInRange(sourcePosition, closestPointOnCollider);
            }

            Rigidbody targetRigidBody = interactionEvent.Target.GetComponent<Rigidbody>();
            if (targetRigidBody != null)
            {
                Vector3 closestPointOnRigidBody = targetRigidBody.ClosestPointOnBounds(sourcePosition);
                return range.IsInRange(sourcePosition, closestPointOnRigidBody);
            }

            return false;
        }
    }
}
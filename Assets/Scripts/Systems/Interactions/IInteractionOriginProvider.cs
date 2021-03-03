using UnityEngine;

namespace Spessman.Interactions
{
    public interface IInteractionOriginProvider
    {
        Vector3 InteractionOrigin { get; }
    }
}
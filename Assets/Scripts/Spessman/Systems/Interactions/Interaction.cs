using Spessman.Interactions.Extensions;
using UnityEngine;

namespace Spessman.Interactions
{
    public class Interaction : IInteraction
    {
        public virtual IClientInteraction CreateClient(InteractionEvent interactionEvent)
        {
            return null;
        }

        public virtual string GetName(InteractionEvent interactionEvent)
        {
            return "Interaction";
        }

        public virtual Sprite GetIcon(InteractionEvent interactionEvent)
        {
            return null;
        }

        public virtual bool CanInteract(InteractionEvent interactionEvent)
        {
            // Range check
            if (!InteractionExtensions.RangeCheck(interactionEvent))
            {
                return false;
            }

            return true;
        }

        public virtual bool Start(InteractionEvent interactionEvent, InteractionReference reference)
        {
            return false;
        }

        public virtual bool Update(InteractionEvent interactionEvent, InteractionReference reference)
        {
            return false;
        }

        public virtual void Cancel(InteractionEvent interactionEvent, InteractionReference reference)
        {
            return;
        }
    }
}
namespace Spessman.Interactions
{
    public struct InteractionEntry
    {
        public IInteractionTarget Target;
        public IInteraction Interaction;

        public InteractionEntry(IInteractionTarget target, IInteraction interaction)
        {
            Target = target;
            Interaction = interaction;
        }
    }
}
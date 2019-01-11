using System.Numerics;

namespace Monitors.Domain.Elements
{
    public class Element
    {
        public ElementId Id { get; }

        public ElementKind Kind { get; }

        public string Name { get; private set; }

        public Vector3 Position { get; private set; }

        public void ChangeName(
            string newName,
            IElementEventPublisher eventPublisher)
        {
            if (Name == newName)
                return;

            Name = newName;

            eventPublisher.NameChanged(newName);
        }

        public void ChangePosition(
            Vector3 newPosition,
            IElementEventPublisher eventPublisher)
        {
            if (Position == newPosition)
                return;

            Position = newPosition;

            eventPublisher.PositionChanged(newPosition);
        }
    }
}
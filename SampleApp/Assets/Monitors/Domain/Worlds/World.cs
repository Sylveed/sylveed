using System.Collections.Generic;
using Monitors.Domain.Elements;

namespace Monitors.Domain.Worlds
{
    public class World
    {
        readonly HashSet<ElementId> elementIds =
            new HashSet<ElementId>();

        public WorldId Id { get; }

        public string Name { get; }

        public void CreateElement(
            string name, 
            ElementKind kind, 
            IElementFactory factory,
            IWorldEventPublisher eventPublisher)
        {
            var element = factory.Create(name, kind);

            elementIds.Add(element.Id);

            eventPublisher.ElementCreated(Id, element);
        }

        public void DeleteElement(
            ElementId elementId,
            IWorldEventPublisher eventPublisher)
        {
            if (elementIds.Contains(elementId))
            {
                elementIds.Remove(elementId);

                eventPublisher.ElementDeleted(Id, elementId);
            }
        }
    }
}
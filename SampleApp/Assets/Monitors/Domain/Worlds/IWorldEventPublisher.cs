using Monitors.Domain.Elements;

namespace Monitors.Domain.Worlds
{
    public interface IWorldEventPublisher
    {
        void ElementCreated(WorldId worldId, Element element);
        void ElementDeleted(WorldId worldId, ElementId elementId);
    }
}
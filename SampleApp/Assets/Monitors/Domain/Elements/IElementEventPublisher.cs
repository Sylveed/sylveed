using System.Numerics;

namespace Monitors.Domain.Elements
{
    public interface IElementEventPublisher
    {
        void NameChanged(string newName);
        void PositionChanged(Vector3 newPosition);
    }
}
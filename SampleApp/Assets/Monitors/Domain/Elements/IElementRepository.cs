namespace Monitors.Domain.Elements
{
    public interface IElementRepository
    {
        Element Find(ElementId id);
    }
}
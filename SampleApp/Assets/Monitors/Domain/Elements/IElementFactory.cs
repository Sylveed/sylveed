
namespace Monitors.Domain.Elements
{
    public interface IElementFactory
    {
        Element Create(string name, ElementKind kind);
    }
}
using System.Collections.Generic;
using Monitors.Domain.Elements;

namespace Monitors.Application.Selections
{
    public interface IRectangleRangeSelectService
    {
        IEnumerable<Element> SelectElements(RectangleSelectionRange range);
    }
}
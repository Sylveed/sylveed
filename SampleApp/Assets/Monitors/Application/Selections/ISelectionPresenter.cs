using System.Collections.Generic;
using Monitors.Domain.Elements;

namespace Monitors.Application.Selections
{
    public interface ISelectionPresenter
    {
        void Select(ElementId elementId);
        void Select(IEnumerable<ElementId> elementIds);
        void ClearSelection();
    }
}
using System.Linq;
using Monitors.Application.Selections;
using Monitors.Domain.Elements;

namespace Monitors.UseCase.Edits
{
    public class SelectElement
    {
        readonly ISelectionPresenter selectionPresenter;
        readonly IRectangleRangeSelectService rangeSelectService;

        public void Select(ElementId elementId)
        {
            selectionPresenter.Select(elementId);
        }

        public void ClearSelection()
        {
            selectionPresenter.ClearSelection();
        }

        public void SelectRange(RectangleSelectionRange range)
        {
            var elementIds = rangeSelectService.SelectElements(range)
                .Select(x => x.Id);

            selectionPresenter.Select(elementIds);
        }
    }
}
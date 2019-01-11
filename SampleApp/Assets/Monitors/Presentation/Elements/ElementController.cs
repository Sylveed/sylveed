using System;
using Monitors.Domain.Elements;
using Monitors.UseCase.Elements;

namespace Monitors.Presentation.Elements
{
    public class ElementController
    {
        public void CreateElement(string name, ElementKindDto elementKind)
        {

        }

        public void DeleteElement(string worldId, string elementId)
        {

        }

        ElementKind ConvertToElementKind(ElementKindDto source)
        {
            switch (source)
            {
                case ElementKindDto.Device:
                    return ElementKind.Device;
                default: 
                    throw new ArgumentException();
            }
        }
    }
}
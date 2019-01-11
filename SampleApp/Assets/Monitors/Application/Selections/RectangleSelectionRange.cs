using System.Numerics;
using UnityEngine;

namespace Monitors.Application.Selections
{
    public struct RectangleSelectionRange
    {
        public Rect RectInScreenCoordinate { get; set; }

        public RectangleSelectionRange(Rect rectInScreenCoordinate)
        {
            RectInScreenCoordinate = rectInScreenCoordinate;
        }
    }
}
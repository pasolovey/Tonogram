using RenderTest.Render;
using System;
using System.Windows;
using System.Windows.Media;

namespace Tonogram.Render
{
    class TwoPointBezierPhoneticShape : PhoneticShape
    {
        public TwoPointBezierPhoneticShape(string text)
            : base(text)
        {
        }

        protected override void DrawFigure(DrawingContext dc)
        {
            var rect = GetShapeRect();
            var verticalSize = rect.Height;
            var levelStep = verticalSize / (LevelsCount - 1);
            var lvl1 = Math.Max(StartLevel - 1, 0);
            var lvl2 = Math.Max(StopLevel - 1, 0);
            var point1 = new Point(width / 2 - 10, rect.Top + ((LevelsCount - 1) - lvl1) * levelStep);
            var point2 = new Point(width / 2 + 10, rect.Top + ((LevelsCount - 1) - lvl2) * levelStep);

            PathGeometry pathGeom = new PathGeometry();
            PathFigure pf = new PathFigure();
            pf.StartPoint = point1;
            pf.Segments.Add(new QuadraticBezierSegment(new Point(point2.X, point1.Y), point2, true));
            pathGeom.Figures.Add(pf);
            dc.DrawGeometry(null, DefaultPen, pathGeom);
        }
    }
}

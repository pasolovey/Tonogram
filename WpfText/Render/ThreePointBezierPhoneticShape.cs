using RenderTest.Render;
using System;
using System.Windows;
using System.Windows.Media;

namespace WpfText.Render
{
    class ThreePointBezierPhoneticShape : PhoneticShape
    {
        private double LeftPartOffset = 20d;
        private double RightPartOffset = 20d;
        private double ControlPointOffset = 10d;

        public ThreePointBezierPhoneticShape(string text)
            : base(text)
        {
        }

        public int MidLevel { get; set; }

        protected override void DrawFigure(DrawingContext dc)
        {
            var rect = GetShapeRect();
            var verticalSize = rect.Height;
            var levelStep = verticalSize / (LevelsCount - 1);
            var lvl1 = Math.Max(StartLevel - 1, 0);
            var lvl2 = Math.Max(MidLevel - 1, 0);
            var lvl3 = Math.Max(StopLevel - 1, 0);

            var point1 = new Point(width / 2 - LeftPartOffset, rect.Top + ((LevelsCount - 1) - lvl1) * levelStep);
            var point2 = new Point(width / 2, rect.Top + ((LevelsCount - 1) - lvl2) * levelStep);
            var point3 = new Point(width / 2 + RightPartOffset, rect.Top + ((LevelsCount - 1) - lvl3) * levelStep);

            PathGeometry pathGeom = new PathGeometry();
            PathFigure pf = new PathFigure();
            pf.StartPoint = point1;

            pf.Segments.Add(new QuadraticBezierSegment(new Point(point2.X - ControlPointOffset, point2.Y), point2, true));
            pf.Segments.Add(new QuadraticBezierSegment(new Point(point2.X + ControlPointOffset, point2.Y), point3, true));

            pathGeom.Figures.Add(pf);
            dc.DrawGeometry(null, DefaultPen, pathGeom);
        }

        protected override double MinWidth()
        {
            return Math.Max(base.MinWidth(), LeftPartOffset + RightPartOffset);
        }
    }
}

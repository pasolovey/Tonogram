using RenderTest.Render;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace WpfText.Render
{
    class StresPhoneticShape : PhoneticWithShape
    {
        public StresPhoneticShape(string text)
            : base(text)
        {
        }

        protected override void DrawFigure(DrawingContext dc)
        {
            var verticalSize = defaultHeight - textAreaMaxHeight;
            var levelStep = verticalSize / (LevelsCount - 1);
            var lvl = Math.Max(StartLevel - 1, 0);
            var point = new Point(width / 2, textAreaMaxHeight + ((LevelsCount - 1) - lvl) * levelStep);
            dc.PushTransform(new TranslateTransform(point.X, point.Y));
            dc.DrawLine(BlackPenBold, new Point(-5, 0), new Point(5, 0));
            dc.Pop();
        }
    }

    class MonotonicBezierPhoneticShape : PhoneticWithShape
    {
        public MonotonicBezierPhoneticShape(string text)
            : base(text)
        {
        }

        protected override void DrawFigure(DrawingContext dc)
        {
            var verticalSize = defaultHeight - textAreaMaxHeight;
            var levelStep = verticalSize / (LevelsCount - 1);
            var lvl1 = Math.Max(StartLevel - 1, 0);
            var lvl2 = Math.Max(StopLevel - 1, 0);
            var point1 = new Point(width / 2 - 10, textAreaMaxHeight + ((LevelsCount - 1) - lvl1) * levelStep);
            var point2 = new Point(width / 2 + 10, textAreaMaxHeight + ((LevelsCount - 1) - lvl2) * levelStep);

            PathGeometry pathGeom = new PathGeometry();
            PathFigure pf = new PathFigure();
            pf.StartPoint = point1;
            pf.Segments.Add(new QuadraticBezierSegment(new Point(point2.X, point1.Y), point2, true));
            pathGeom.Figures.Add(pf);
            dc.DrawGeometry(null, DefaultPen, pathGeom);
        }
    }
}

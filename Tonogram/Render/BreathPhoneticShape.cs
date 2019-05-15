using RenderTest.Render;
using System.Windows;
using System.Windows.Media;

namespace Tonogram.Render
{
    class BreathPhoneticShape : PhoneticShape
    {
        public BreathPhoneticShape(string text)
            : base(text)
        {
        }

        protected override void DrawFigure(DrawingContext dc)
        {
            var rect = GetShapeRect();
            var verticalSize = rect.Height;
            dc.DrawLine(DefaultPen, new Point(rect.Width / 2 - 5, rect.Top), new Point(rect.Width / 2, rect.Bottom));
            dc.DrawLine(DefaultPen, new Point(rect.Width / 2, rect.Bottom), new Point(rect.Width / 2, rect.Top + 4));
            dc.DrawLine(DefaultPen, new Point(rect.Width / 2, rect.Top + 4), new Point(rect.Width / 2 + 8, rect.Top + 4));
        }
    }
}

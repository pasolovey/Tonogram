using RenderTest.Render;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;

namespace WpfText.Render
{
    class SpecialRisePhoneticShape : PhoneticShape
    {
        public SpecialRisePhoneticShape(string text)
            : base(text)
        {
        }

        public int Count { get; set; }

        protected override void DrawFigure(DrawingContext dc)
        {
            var rect = GetShapeRect();
            var verticalSize = rect.Height;
            dc.DrawLine(DefaultPen, new Point(rect.Width / 2, rect.Top), new Point(rect.Width / 2, rect.Bottom));
            dc.DrawLine(DefaultPen, new Point(rect.Width / 2, rect.Top), new Point(rect.Width / 2 - 5, rect.Top + 5));
            dc.DrawLine(DefaultPen, new Point(rect.Width / 2, rect.Top), new Point(rect.Width / 2 + 5, rect.Top + 5));
            dc.DrawLine(BlackPenBold, new Point(rect.Width / 2 + 4, rect.Top), new Point(rect.Width / 2 + 14, rect.Top));
        }
    }
}

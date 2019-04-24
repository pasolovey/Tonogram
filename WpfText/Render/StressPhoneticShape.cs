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
    class StressPhoneticShape : PhoneticShape
    {
        public StressPhoneticShape(string text)
            : base(text)
        {
        }

        protected override void DrawFigure(DrawingContext dc)
        {
            var rect = GetShapeRect();
            var verticalSize = rect.Height;
            var levelStep = verticalSize / (LevelsCount - 1);
            var lvl = Math.Max(StartLevel - 1, 0);
            var point = new Point(rect.Width / 2, rect.Top + ((LevelsCount - 1) - lvl) * levelStep);
            dc.PushTransform(new TranslateTransform(point.X, point.Y));
            dc.DrawLine(BlackPenBold, new Point(-5, 0), new Point(5, 0));
            dc.Pop();
        }
    }
}

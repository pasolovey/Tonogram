using RenderTest.Render;
using System;
using System.Windows;
using System.Windows.Media;

namespace Tonogram.Render
{
    class NoStressPhoneticShape : PhoneticShape
    {
        public NoStressPhoneticShape(string text)
            : base(text)
        {
        }

        public PhoneticShape Prev { get; set; }

        protected override void DrawFigure(DrawingContext dc)
        {
            var rect = GetShapeRect();
            var verticalSize = rect.Height;
            var levelStep = verticalSize / (LevelsCount - 1);
            var lvl = Math.Max(StartLevel - 1, 0);
            var point = new Point(width / 2, rect.Top + ((LevelsCount - 1) - lvl) * levelStep);
            dc.PushTransform(new TranslateTransform(point.X, point.Y));
            dc.DrawEllipse(OpacityBlackBrush, null, new Point(0, 0), 3, 3);
            dc.Pop();
        }
    }
}

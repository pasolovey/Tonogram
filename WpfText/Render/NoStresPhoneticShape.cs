using RenderTest.Render;
using System;
using System.Windows;
using System.Windows.Media;

namespace WpfText.Render
{
    class NoStresPhoneticShape : PhoneticWithShape
    {
        public NoStresPhoneticShape(string text)
            : base(text)
        {
        }

        public PhoneticWithShape Prev { get; set; }

        protected override void DrawFigure(DrawingContext dc)
        {
            var verticalSize = defaultHeight - textAreaMaxHeight;
            var levelStep = verticalSize / (LevelsCount - 1);
            var lvl = Math.Max(StartLevel - 1, 0);
            var point = new Point(width / 2, textAreaMaxHeight + ((LevelsCount - 1) - lvl) * levelStep);
            dc.PushTransform(new TranslateTransform(point.X, point.Y));
            dc.DrawEllipse(Brushes.Black, null, new Point(0, 0), 4, 4);
            dc.Pop();
        }
    }
}

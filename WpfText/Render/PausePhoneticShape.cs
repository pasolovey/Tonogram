using RenderTest.Render;
using System.Windows;
using System.Windows.Media;

namespace WpfText.Render
{
    class PausePhoneticShape : PhoneticWithShape
    {
        public PausePhoneticShape(string text)
            : base(text)
        {
        }

        public int Count { get; set; }

        protected override void DrawFigure(DrawingContext dc)
        {
            var rect = GetShapeRect();
            var verticalSize = rect.Height;

            switch(Count)
            {
                case 1:
                    dc.DrawLine(DefaultPen, new Point(rect.Width/2, rect.Top), new Point(rect.Width/2,rect.Bottom));
                    break;
                case 2:
                    dc.DrawLine(DefaultPen, new Point(rect.Width / 2 - 2, rect.Top), new Point(rect.Width / 2 - 2, rect.Bottom));
                    dc.DrawLine(DefaultPen, new Point(rect.Width / 2 + 2, rect.Top), new Point(rect.Width / 2 + 2, rect.Bottom));
                    break;
                case 3:
                    dc.DrawLine(DefaultPen, new Point(rect.Width / 2, rect.Top), new Point(rect.Width / 2, rect.Bottom));
                    dc.DrawLine(DefaultPen, new Point(rect.Width / 2 - 2, rect.Top), new Point(rect.Width / 2 - 2, rect.Bottom));
                    dc.DrawLine(DefaultPen, new Point(rect.Width / 2 + 2, rect.Top), new Point(rect.Width / 2 + 2, rect.Bottom));
                    break;
                default:
                    break;
            }
        }
    }
}

using System.Windows;
using System.Windows.Media;

namespace RenderTest.Render
{
    public class PhoneticShape : PhoneticItemBase
    {
        private double renderAreaPadding = 5;

        private Rect border;

        public PhoneticShape(string text)
        {
            SetText(text);
        }

        public int LevelsCount { get; } = 9;

        public int StartLevel { get; set; }

        public int StopLevel { get; set; }

        protected override void ClculateBorders()
        {
            base.ClculateBorders();
            border = new Rect(0, textAreaMaxHeight, width, defaultHeight - textAreaMaxHeight);
        }

        protected override void OnDrawInternal(DrawingContext dc)
        {
            base.OnDrawInternal(dc);
            if (FormattedText != null)
                DrawTextArea(dc);
            DrawBorders(dc);
            DrawFigure(dc);
        }

        protected virtual void DrawFigure(DrawingContext dc)
        {
        }

        protected virtual Rect GetShapeRect()
        {
            var tmp = border;
            tmp.Inflate(0, -renderAreaPadding);
            return tmp;
        }

        private void DrawTextArea(DrawingContext dc)
        {
            dc.DrawText(FormattedText, new Point(width / 2 - TextWidth / 2, textAreaMaxHeight / 2 - TextHeight / 2));
        }

        private void DrawBorders(DrawingContext dc)
        {
            dc.DrawLine(OpacityDefaultPen, border.TopLeft, border.TopRight);
            dc.DrawLine(OpacityDefaultPen, border.BottomLeft, border.BottomRight);
        }
    }
}

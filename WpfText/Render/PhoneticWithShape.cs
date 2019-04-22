using System.Windows;
using System.Windows.Media;

namespace RenderTest.Render
{
    public class PhoneticWithShape : PhoneticItemBase
    {
        public int StartLevel { get; set; }

        public int StopLevel { get; set; }

        private readonly int levelsCount = 9;
        private double renderAreaPadding = 5;

        public int LevelsCount => levelsCount;
        private Rect border;

        public PhoneticWithShape(string text)
        {
            SetText(text);
        }

        protected override void ClculateBorders()
        {
            base.ClculateBorders();
            border = new Rect(0, textAreaMaxHeight, width, defaultHeight - textAreaMaxHeight);
        }

        void DrawTextArea(DrawingContext dc)
        {
            dc.DrawText(FormattedText, new Point(width / 2 - TextWidth / 2, textAreaMaxHeight / 2 - TextHeight / 2));
        }

        protected override void OnDrawInternal(DrawingContext dc)
        {
            base.OnDrawInternal(dc);
            if (FormattedText != null)
                DrawTextArea(dc);
            DrawLevels(dc);
            DrawFigure(dc);
        }

        void DrawLevels(DrawingContext dc)
        {
            dc.DrawLine(OpacityDefaultPen, border.TopLeft, border.TopRight);
            dc.DrawLine(OpacityDefaultPen, border.BottomLeft, border.BottomRight);

            //var r = GetShapeRect();
            //var levelStep = r.Height / (levelsCount - 1);
            //for (int i = 0; i < levelsCount; i++)
            //{
            //    var y = r.Top + i * levelStep;
            //    dc.DrawLine(OpacityDefaultPen, new Point(r.Left, y), new Point(r.Right, y));
            //}
        }

        protected virtual void DrawFigure(DrawingContext dc)
        {
        }

        protected Rect GetShapeRect()
        {
            var tmp = border;
            tmp.Inflate(0, -renderAreaPadding);
            return tmp;
        }
    }

    
}

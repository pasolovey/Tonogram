using System.Windows;
using System.Windows.Media;

namespace RenderTest.Render
{
    public class PhoneticItemLevels : PhoneticItemBase
    {
        private readonly int levelsCount = 9;

        public int LevelsCount => levelsCount;

        public PhoneticItemLevels(string text)
        {
            SetText(text);
        }

        protected override void OnDrawInternal(DrawingContext dc)
        {
            DrawLevels(dc, new Point(0, textAreaMaxHeight));
            if (FormattedText != null)
                DrawTextArea(dc);
        }

        void DrawTextArea(DrawingContext dc)
        {
            dc.DrawText(FormattedText, new Point(width / 2 - TextWidth / 2, textAreaMaxHeight / 2 - TextHeight / 2));
        }

        void DrawLevels(DrawingContext dc, Point position)
        {
            var verticalSize = defaultHeight - textAreaMaxHeight;
            var levelStep = verticalSize / (levelsCount - 1);
            dc.DrawLine(OpacityDefaultPen, new Point(position.X, textAreaMaxHeight), new Point(position.X + width, textAreaMaxHeight));
            dc.DrawLine(OpacityDefaultPen, new Point(position.X, defaultHeight), new Point(position.X + width, defaultHeight));

            for (int i = 0; i < levelsCount; i++)
            {
                var y = position.Y + i * levelStep;
                //dc.DrawLine(OpacityDefaultPen, new Point(position.X, y), new Point(position.X + width, y));
            }
        }
    }
}

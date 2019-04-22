using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace RenderTest.Render
{
    public abstract class PhoneticItemBase : IRenderable
    {
        protected double width = 0;
        protected double height = 0;
        protected double minWidth = 30;
        protected double textPaddingHor = 5;
        protected double textPaddingVer = 5;
        protected double textAreaMaxHeight = 20;

        public static Pen DefaultPen = new Pen(Brushes.Black, 1);
        public static Pen BlackPenBold = new Pen(Brushes.Black, 3);
        public static Brush OpacityBlackBrush = new SolidColorBrush(Color.FromArgb(128, 0, 0, 0));
        public static Pen OpacityDefaultPen = new Pen(OpacityBlackBrush, 1);
        public static Pen HighlightPen = new Pen(Brushes.Red, 1);

        private string text;
        private FormattedText formattedText;
        private int fontSize = 12;
        private double textHeight;
        private double textWidth;

        protected readonly double defaultHeight = 100;

        public PhoneticItemBase()
        {
            ClculateBorders();
        }

        public double Width => width;
        public double Height => height;

        public double TextHeight { get => textHeight; set => textHeight = value; }
        public double TextWidth { get => textWidth; set => textWidth = value; }
        public FormattedText FormattedText { get => formattedText; private set => formattedText = value; }

        public void SetText(string text)
        {
            this.text = text;
            if (!string.IsNullOrEmpty(text))
            {
                UpdateFormatedText();
                ClculateBorders();
            }
        }

        public void OnDraw(DrawingContext dc, Point position)
        {
            dc.PushTransform(new TranslateTransform(position.X, position.Y));
            OnDrawInternal(dc);
            dc.Pop();
        }

        protected virtual void OnDrawInternal(DrawingContext dc)
        {

        }

        private void UpdateFormatedText()
        {
            FormattedText = new FormattedText(text,
                CultureInfo.GetCultureInfo("en-us"), 
                FlowDirection.LeftToRight, 
                new Typeface("Verdana"), 
                fontSize, 
                Brushes.Black);
            TextWidth = FormattedText.Width;
            TextHeight = FormattedText.Height;
            width = Math.Max(TextWidth, minWidth) + textPaddingHor;
        }

        protected virtual void ClculateBorders()
        {
            height = defaultHeight;
            width = Math.Max(TextWidth, minWidth) + textPaddingHor;
        }
    }
}

using ICSharpCode.AvalonEdit.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace WpfText.Colorizers
{
    class LineColorizer : DocumentColorizingTransformer
    {
        int lineNumber;

        public LineColorizer(int lineNumber)
        {
            this.lineNumber = lineNumber;
        }

        protected override void ColorizeLine(ICSharpCode.AvalonEdit.Document.DocumentLine line)
        {
            if (!line.IsDeleted && line.LineNumber == lineNumber)
            {
                ChangeLinePart(line.Offset, line.EndOffset, ApplyChanges);
            }
        }

        protected override void Colorize(ITextRunConstructionContext context)
        {
            base.Colorize(context);
        }

        void ApplyChanges(VisualLineElement element)
        {
            // This is where you do anything with the line
            element.TextRunProperties.SetForegroundBrush(Brushes.Red);
        }
    }
}

using ICSharpCode.AvalonEdit;
using System;
using System.Windows.Input;

namespace Tonogram.Commands
{
    public class LowFallCommand : BaseToolCommand
    {
        public TextEditor Avalon { get; set; }

        public override string Name => "LF";

        public LowFallCommand(TextEditor text)
        {
            Avalon = text;
            tooltip = "Low fall tone";
        }

        public override void Execute(object parameter)
        {
            var offset = Avalon.Document.GetOffset(Avalon.TextArea.Caret.Location);
            Avalon.Document.Insert(offset, "%lf%");
        }
    }
}

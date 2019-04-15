using ICSharpCode.AvalonEdit;
using System;
using System.Windows.Input;

namespace WpfText.Commands
{
    public class HighFallCommand : BaseToolCommand
    {
        public TextEditor Avalon { get; set; }

        public override string Name => "HF";

        public HighFallCommand(TextEditor text)
        {
            Avalon = text;
        }

        public override void Execute(object parameter)
        {
            var offset = Avalon.Document.GetOffset(Avalon.TextArea.Caret.Location);
            Avalon.Document.Insert(offset, "%hf%");
        }
    }
}

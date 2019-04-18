using ICSharpCode.AvalonEdit;
using System;
using System.Windows.Input;

namespace WpfText.Commands
{
    public class StressCommand : BaseToolCommand
    {
        public TextEditor Avalon { get; set; }

        public override string Name => "ST";

        public StressCommand(TextEditor text)
        {
            Avalon = text;
            tooltip = "Stress tone";
        }

        public override void Execute(object parameter)
        {
            var offset = Avalon.Document.GetOffset(Avalon.TextArea.Caret.Location);
            Avalon.Document.Insert(offset, "#");
        }
    }
}

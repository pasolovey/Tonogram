using ICSharpCode.AvalonEdit;
using System;
using System.Windows.Input;

namespace WpfText.Commands
{
    public class MidFallCommand : BaseToolCommand
    {
        public TextEditor Avalon { get; set; }

        public override string Name => "MF";

        public MidFallCommand(TextEditor text)
        {
            Avalon = text;
        }

        public override void Execute(object parameter)
        {
            var offset = Avalon.Document.GetOffset(Avalon.TextArea.Caret.Location);
            Avalon.Document.Insert(offset, "%mf%");
        }
    }
}

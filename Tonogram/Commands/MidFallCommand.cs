using ICSharpCode.AvalonEdit;
using System;
using System.Windows.Input;

namespace Tonogram.Commands
{
    public class MidFallCommand : BaseToolCommand
    {
        public TextEditor Avalon { get; set; }

        public override string Name => "MF";

        public MidFallCommand(TextEditor text)
        {
            Avalon = text;
            tooltip = "Middle fall tone";
        }

        public override void Execute(object parameter)
        {
            var offset = Avalon.Document.GetOffset(Avalon.TextArea.Caret.Location);
            Avalon.Document.Insert(offset, "%mf%");
        }
    }
}

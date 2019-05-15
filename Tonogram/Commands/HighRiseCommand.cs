using ICSharpCode.AvalonEdit;

namespace Tonogram.Commands
{
    public class HighRiseCommand : BaseToolCommand
    {
        public TextEditor Avalon { get; set; }

        public override string Name => "HR";

        public HighRiseCommand(TextEditor text)
        {
            Avalon = text;
            tooltip = "High rise tone";
        }

        public override void Execute(object parameter)
        {
            var offset = Avalon.Document.GetOffset(Avalon.TextArea.Caret.Location);
            Avalon.Document.Insert(offset, "%hr%");
        }
    }
}

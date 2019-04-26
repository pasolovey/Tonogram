using ICSharpCode.AvalonEdit;

namespace WpfText.Commands
{
    public class LowRiseCommand : BaseToolCommand
    {
        public TextEditor Avalon { get; set; }

        public override string Name => "LR";

        public LowRiseCommand(TextEditor text)
        {
            Avalon = text;
            tooltip = "Low rise tone";
        }

        public override void Execute(object parameter)
        {
            var offset = Avalon.Document.GetOffset(Avalon.TextArea.Caret.Location);
            Avalon.Document.Insert(offset, "%lr%");
        }
    }
}

using ICSharpCode.AvalonEdit;

namespace WpfText.Commands
{
    public class FallRiseCommand : BaseToolCommand
    {
        public TextEditor Avalon { get; set; }

        public override string Name => "FR";

        public FallRiseCommand(TextEditor text)
        {
            Avalon = text;
            tooltip = "Fall rise tone";
        }

        public override void Execute(object parameter)
        {
            var offset = Avalon.Document.GetOffset(Avalon.TextArea.Caret.Location);
            Avalon.Document.Insert(offset, "%fr%");
        }
    }
}

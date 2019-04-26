using ICSharpCode.AvalonEdit;

namespace WpfText.Commands
{
    public class SpecialRiseCommand : BaseToolCommand
    {
        public TextEditor Avalon { get; set; }

        public override string Name => "SR";

        public SpecialRiseCommand(TextEditor text)
        {
            Avalon = text;
            tooltip = "Special rise tone";
        }

        public override void Execute(object parameter)
        {
            var offset = Avalon.Document.GetOffset(Avalon.TextArea.Caret.Location);
            Avalon.Document.Insert(offset, "%sr%");
        }
    }
}

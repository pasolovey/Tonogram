using ICSharpCode.AvalonEdit;

namespace WpfText.Commands
{
    public class SlidingToneCommand : BaseToolCommand
    {
        public TextEditor Avalon { get; set; }

        public override string Name => "^";

        public SlidingToneCommand(TextEditor text)
        {
            Avalon = text;
        }

        public override void Execute(object parameter)
        {
            var offset = Avalon.Document.GetOffset(Avalon.TextArea.Caret.Location);
            Avalon.Document.Insert(offset, "^");
        }
    }
}

using ICSharpCode.AvalonEdit;

namespace WpfText.Commands
{
    public class LongPauseCommand : BaseToolCommand
    {
        public TextEditor Avalon { get; set; }

        public override string Name => "||";

        public LongPauseCommand(TextEditor text)
        {
            Avalon = text;
        }

        public override void Execute(object parameter)
        {
            var offset = Avalon.Document.GetOffset(Avalon.TextArea.Caret.Location);
            Avalon.Document.Insert(offset, "||");
        }
    }
}

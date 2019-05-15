using ICSharpCode.AvalonEdit;

namespace Tonogram.Commands
{
    public class ShortPauseCommand : BaseToolCommand
    {
        public TextEditor Avalon { get; set; }

        public override string Name => "|";

        public ShortPauseCommand(TextEditor text)
        {
            Avalon = text;
        }

        public override void Execute(object parameter)
        {
            var offset = Avalon.Document.GetOffset(Avalon.TextArea.Caret.Location);
            Avalon.Document.Insert(offset, "|");
        }
    }
}

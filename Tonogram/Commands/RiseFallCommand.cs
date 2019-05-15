using ICSharpCode.AvalonEdit;

namespace Tonogram.Commands
{
    public class RiseFallCommand : BaseToolCommand
    {
        public TextEditor Avalon { get; set; }

        public override string Name => "RF";

        public RiseFallCommand(TextEditor text)
        {
            Avalon = text;
            tooltip = "Rise fall tone";
        }

        public override void Execute(object parameter)
        {
            var offset = Avalon.Document.GetOffset(Avalon.TextArea.Caret.Location);
            Avalon.Document.Insert(offset, "%rf%");
        }
    }
}

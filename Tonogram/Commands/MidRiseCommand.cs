using ICSharpCode.AvalonEdit;

namespace Tonogram.Commands
{
    public class MidRiseCommand : BaseToolCommand
    {
        public TextEditor Avalon { get; set; }

        public override string Name => "MR";

        public MidRiseCommand(TextEditor text)
        {
            Avalon = text;
            tooltip = "Middle rise tone";
        }

        public override void Execute(object parameter)
        {
            var offset = Avalon.Document.GetOffset(Avalon.TextArea.Caret.Location);
            Avalon.Document.Insert(offset, "%mr%");
        }
    }
}

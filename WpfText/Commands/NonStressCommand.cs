using ICSharpCode.AvalonEdit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace WpfText.Commands
{
    public class NonStressCommand : BaseToolCommand
    {
        public TextEditor Avalon { get; set; }

        public override string Name => "NS";

        public NonStressCommand(TextEditor text)
        {
            Avalon = text;
            tooltip = "Non stress tone";
        }

        public override void Execute(object parameter)
        {
            var offset = Avalon.Document.GetOffset(Avalon.TextArea.Caret.Location);
            Avalon.Document.Insert(offset, "*");
        }
    }
}

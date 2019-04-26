using ICSharpCode.AvalonEdit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfText.Commands
{
    public class HighLevelCommand : BaseToolCommand
    {
        public TextEditor Avalon { get; set; }

        public override string Name => "HL";

        public HighLevelCommand(TextEditor text)
        {
            Avalon = text;
            tooltip = "High level tone";
        }

        public override void Execute(object parameter)
        {
            var offset = Avalon.Document.GetOffset(Avalon.TextArea.Caret.Location);
            Avalon.Document.Insert(offset, "%hl%");
        }
    }

    public class LowLevelCommand : BaseToolCommand
    {
        public TextEditor Avalon { get; set; }

        public override string Name => "LL";

        public LowLevelCommand(TextEditor text)
        {
            Avalon = text;
            tooltip = "Low level tone";
        }

        public override void Execute(object parameter)
        {
            var offset = Avalon.Document.GetOffset(Avalon.TextArea.Caret.Location);
            Avalon.Document.Insert(offset, "%ll%");
        }
    }

    public class MidLevelCommand : BaseToolCommand
    {
        public TextEditor Avalon { get; set; }

        public override string Name => "ML";

        public MidLevelCommand(TextEditor text)
        {
            Avalon = text;
            tooltip = "Middle level tone";
        }

        public override void Execute(object parameter)
        {
            var offset = Avalon.Document.GetOffset(Avalon.TextArea.Caret.Location);
            Avalon.Document.Insert(offset, "%ml%");
        }
    }

    public class HighFallIncompletCommand : BaseToolCommand
    {
        public TextEditor Avalon { get; set; }

        public override string Name => "HI";

        public HighFallIncompletCommand(TextEditor text)
        {
            Avalon = text;
        }

        public override void Execute(object parameter)
        {
            var offset = Avalon.Document.GetOffset(Avalon.TextArea.Caret.Location);
            Avalon.Document.Insert(offset, "%hi%");
        }
    }

    public class LowFallIncompletCommand : BaseToolCommand
    {
        public TextEditor Avalon { get; set; }

        public override string Name => "LI";

        public LowFallIncompletCommand(TextEditor text)
        {
            Avalon = text;
        }

        public override void Execute(object parameter)
        {
            var offset = Avalon.Document.GetOffset(Avalon.TextArea.Caret.Location);
            Avalon.Document.Insert(offset, "%li%");
        }
    }
}

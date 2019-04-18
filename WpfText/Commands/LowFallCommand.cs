using ICSharpCode.AvalonEdit;
using System;
using System.Windows.Input;

namespace WpfText.Commands
{
    public class LowFallCommand : BaseToolCommand
    {
        public TextEditor Avalon { get; set; }

        public override string Name => "LF";

        public LowFallCommand(TextEditor text)
        {
            Avalon = text;
            tooltip = "Low fall tone";
        }

        public override void Execute(object parameter)
        {
            var offset = Avalon.Document.GetOffset(Avalon.TextArea.Caret.Location);
            Avalon.Document.Insert(offset, "%lf%");
        }
    }



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

    public class HighRiseCommand : BaseToolCommand
    {
        public TextEditor Avalon { get; set; }

        public override string Name => "HR";

        public HighRiseCommand(TextEditor text)
        {
            Avalon = text;
            tooltip = "High rise tone";
        }

        public override void Execute(object parameter)
        {
            var offset = Avalon.Document.GetOffset(Avalon.TextArea.Caret.Location);
            Avalon.Document.Insert(offset, "%hr%");
        }
    }


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

    public class LongPauseCommand : BaseToolCommand
    {
        public TextEditor Avalon { get; set; }

        public override string Name => "|||";

        public LongPauseCommand(TextEditor text)
        {
            Avalon = text;
        }

        public override void Execute(object parameter)
        {
            var offset = Avalon.Document.GetOffset(Avalon.TextArea.Caret.Location);
            Avalon.Document.Insert(offset, "|||");
        }
    }

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

    public class PauseCommand : BaseToolCommand
    {
        public TextEditor Avalon { get; set; }

        public override string Name => "||";

        public PauseCommand(TextEditor text)
        {
            Avalon = text;
        }

        public override void Execute(object parameter)
        {
            var offset = Avalon.Document.GetOffset(Avalon.TextArea.Caret.Location);
            Avalon.Document.Insert(offset, "||");
        }
    }
    public class BreathCommand : BaseToolCommand
    {
        public TextEditor Avalon { get; set; }

        public override string Name => "/";

        public BreathCommand(TextEditor text)
        {
            Avalon = text;
        }

        public override void Execute(object parameter)
        {
            var offset = Avalon.Document.GetOffset(Avalon.TextArea.Caret.Location);
            Avalon.Document.Insert(offset, "/");
        }
    }
}

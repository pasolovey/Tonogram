using ICSharpCode.AvalonEdit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace WpfText.Commands
{
    public class LowFallCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;
        public TextEditor Avalon { get; set; }

        public LowFallCommand(TextEditor text)
        {
            Avalon = text;
        }

        public bool CanExecute(object parameter)
        {
            return !string.IsNullOrEmpty(Avalon.TextArea.Selection.GetText());
        }

        public void Execute(object parameter)
        {
            Avalon.TextArea.Selection.ReplaceSelectionWithText($"3%lf{ Avalon.TextArea.Selection.GetText()}%1");
        }
    }

    public class HighFallCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;
        public TextEditor Avalon { get; set; }

        public HighFallCommand(TextEditor text)
        {
            Avalon = text;
        }

        public bool CanExecute(object parameter)
        {
            return !string.IsNullOrEmpty(Avalon.TextArea.Selection.GetText());
        }

        public void Execute(object parameter)
        {
            Avalon.TextArea.Selection.ReplaceSelectionWithText($"9%hf{ Avalon.TextArea.Selection.GetText()}%1");
        }
    }

    public class MidFallCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;
        public TextEditor Avalon { get; set; }

        public MidFallCommand(TextEditor text)
        {
            Avalon = text;
        }

        public bool CanExecute(object parameter)
        {
            return !string.IsNullOrEmpty(Avalon.TextArea.Selection.GetText());
        }

        public void Execute(object parameter)
        {
            Avalon.TextArea.Selection.ReplaceSelectionWithText($"5%mf{ Avalon.TextArea.Selection.GetText()}%1");
        }
    }

    public class StressCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;
        public TextEditor Avalon { get; set; }

        public StressCommand(TextEditor text)
        {
            Avalon = text;
        }

        public bool CanExecute(object parameter)
        {
            return !string.IsNullOrEmpty(Avalon.TextArea.Selection.GetText());
        }

        public void Execute(object parameter)
        {
            Avalon.TextArea.Selection.ReplaceSelectionWithText($"#{ Avalon.TextArea.Selection.GetText()}");
        }
    }

    public class NonStressCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;
        public TextEditor Avalon { get; set; }

        public NonStressCommand(TextEditor text)
        {
            Avalon = text;
        }

        public bool CanExecute(object parameter)
        {
            return !string.IsNullOrEmpty(Avalon.TextArea.Selection.GetText());
        }

        public void Execute(object parameter)
        {
            Avalon.TextArea.Selection.ReplaceSelectionWithText($"*{ Avalon.TextArea.Selection.GetText()}");
        }
    }
}

using ICSharpCode.AvalonEdit;
using ICSharpCode.AvalonEdit.Document;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using WpfText.Commands;

namespace WpfText
{
    /// <summary>
    /// Interaction logic for Editor.xaml
    /// </summary>
    public partial class EditorView : Window
    {
        public static RoutedCommand MyCommand = new RoutedCommand();
        ShortcutKeyBindings keyBindings = new ShortcutKeyBindings();

        public EditorView()
        {
            InitializeComponent();

            //CommandBinding cb = new CommandBinding(MyCommand, MyCommandExecute, MyCommandCanExecute);
            //AvalonEditor.TextArea.CommandBindings.Add(cb);

            keyBindings.AddShortCut(new KeyGesture(Key.D1, ModifierKeys.Control), new HighFallCommand(AvalonEditor));
            keyBindings.AddShortCut(new KeyGesture(Key.D2, ModifierKeys.Control), new MidFallCommand(AvalonEditor));
            keyBindings.AddShortCut(new KeyGesture(Key.D3, ModifierKeys.Control), new LowFallCommand(AvalonEditor));
            keyBindings.AddShortCut(new KeyGesture(Key.D4, ModifierKeys.Control), new StressCommand(AvalonEditor));
            keyBindings.AddShortCut(new KeyGesture(Key.D5, ModifierKeys.Control), new NonStressCommand(AvalonEditor));
            AvalonEditor.TextArea.InputBindings.AddRange(keyBindings.GetKeyBindings());

            //KeyGesture kg = new KeyGesture(Key.M, ModifierKeys.Control);
            //InputBinding ib = new InputBinding(new InputCommand(AvalonEditor), kg);
            //AvalonEditor.TextArea.InputBindings.Add(ib);
            AvalonEditor.Document.Changed += Document_Changed;
            AvalonEditor.TextArea.FontSize = 20;

            //DataContext = new EditorVM(AvalonEditor.Document, AvalonEditor.TextArea);
            //AvalonEditor.AppendText("sdsds");
            //AvalonEditor.KeyDown += AvalonEditor_KeyDown;
            //AvalonEditor.TextArea.Caret.PositionChanged += Caret_PositionChanged;
            //AvalonEditor.TextArea.PreviewKeyDown += TextArea_KeyDown;
            // AvalonEditor.TextArea.PreviewMouseLeftButtonDown += TextArea_MouseLeftButtonDown;
        }

        private void Document_Changed(object sender, DocumentChangeEventArgs e)
        {
            //AvalonEditor.TextArea.Selection.UpdateOnDocumentChange(e);
        }

        private void TextArea_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            
            //AvalonEditor.Document.Insert(0, "sdsds");
        }

        private void TextArea_KeyDown(object sender, KeyEventArgs e)
{
            //if (e.Key == Key.Back)
            //{
            //    AvalonEditor.Document.Insert(0, "ssss");
            //    AvalonEditor.TextArea.Caret.Offset = AvalonEditor.TextArea.Caret.Offset - 4;
            //    e.Handled = true;
            //}
        }

        private void Caret_PositionChanged(object sender, EventArgs e)
        {
            
        }

        private void TextArea_MouseDown(object sender, MouseButtonEventArgs e)
        {
            //AvalonEditor.Document.UndoStack.Undo();
            //DocumentLine line = AvalonEditor.Document.GetLineByOffset(AvalonEditor.CaretOffset);
            //AvalonEditor.Select(line.Offset, line.Length);
        }


        private void AvalonEditor_KeyDown(object sender, KeyEventArgs e)
        {
            AvalonEditor.Document.Insert(AvalonEditor.Document.TextLength - 1, "sdsds");
            //if (!e.IsRepeat)
            //    if (e.Key == Key.LeftCtrl)
            //        AvalonEditor.AppendText("ctrl");

        }

        private void MyCommandCanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void MyCommandExecute(object sender, ExecutedRoutedEventArgs e)
        {
            var sel = AvalonEditor.TextArea.Selection;
            
            var start = sel.StartPosition;
            var end = sel.EndPosition;

            int offsetStart = AvalonEditor.Document.GetOffset(start.Location);
            int offsetEnd = AvalonEditor.Document.GetOffset(end.Location);
            if (offsetStart > offsetEnd)
            {
                var tmp = offsetEnd;
                offsetEnd = offsetStart;
                offsetStart = tmp;
            }
            var text = AvalonEditor.Document.GetText(offsetStart, offsetEnd - offsetStart);
            
            //AvalonEditor.Document.Insert(offsetStart - 1, "3%lf");
            sel.ReplaceSelectionWithText($"3%lf{sel.GetText()}%1");
            //AvalonEditor.Document.Insert(offsetEnd + 1, "%1");
        }
    }

   

}

using ICSharpCode.AvalonEdit.Document;
using ICSharpCode.AvalonEdit.Highlighting;
using RenderTest.Model;
using System;
using System.Collections;
using System.IO;
using System.Windows;
using System.Windows.Input;
using System.Xml;
using WpfText.Commands;
using WpfText.Model;

namespace WpfText
{
    public partial class EditorView : Window
    {
        public static RoutedCommand MyCommand = new RoutedCommand();
        ShortcutKeyBindings keyBindings = new ShortcutKeyBindings();
        PopupToolController popupToolController;
        

        public EditorView()
        {
            InitializeComponent();
            popupToolController = new PopupToolController(new CommandSource(AvalonEditor));
            IHighlightingDefinition customHighlighting;
            using (Stream s = typeof(EditorView).Assembly.GetManifestResourceStream("WpfText.CustomHighlighting.xshd"))
            {
                if (s == null)
                    throw new InvalidOperationException("Could not find embedded resource");
                using (XmlReader reader = new XmlTextReader(s))
                {
                    customHighlighting = ICSharpCode.AvalonEdit.Highlighting.Xshd.
                        HighlightingLoader.Load(reader, HighlightingManager.Instance);
                    AvalonEditor.SyntaxHighlighting = customHighlighting;
                }
            }
            View.SetModel(new ModelSource(AvalonEditor));


            //CommandBinding cb = new CommandBinding(MyCommand, MyCommandExecute, MyCommandCanExecute);
            //AvalonEditor.TextArea.CommandBindings.Add(cb);
            keyBindings.AddShortCut(new KeyGesture(Key.L|Key.F, ModifierKeys.Control), new HighFallCommand(AvalonEditor));
            //keyBindings.AddShortCut(new KeyGesture(Key.D2, ModifierKeys.Control), new MidFallCommand(AvalonEditor));
            //keyBindings.AddShortCut(new KeyGesture(Key.D3, ModifierKeys.Control), new LowFallCommand(AvalonEditor));
            //keyBindings.AddShortCut(new KeyGesture(Key.D4, ModifierKeys.Control), new StressCommand(AvalonEditor));
            //keyBindings.AddShortCut(new KeyGesture(Key.D5, ModifierKeys.Control), new NonStressCommand(AvalonEditor));
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
            AvalonEditor.TextArea.PreviewKeyDown += TextArea_KeyDown;
            AvalonEditor.TextArea.PreviewKeyUp += TextArea_PreviewKeyUp;
            //AvalonEditor.TextArea.PreviewMouseLeftButtonDown += TextArea_MouseLeftButtonDown;
        }

        private void TextArea_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.RightCtrl && !e.IsRepeat)
            {
                var popupPosition = AvalonEditor.TextArea.TextView.GetVisualPosition(
                    AvalonEditor.TextArea.Caret.Position,
                    ICSharpCode.AvalonEdit.Rendering.VisualYPosition.LineMiddle);
                
                var screenPoint = AvalonEditor.PointToScreen(popupPosition);
                popupToolController.Show(this, screenPoint);
            }
        }

        private void TextArea_PreviewKeyUp(object sender, KeyEventArgs e)
        {
           
        }

        private void Document_Changed(object sender, DocumentChangeEventArgs e)
        {
        }

        private void TextArea_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

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
            sel.ReplaceSelectionWithText($"3%lf{sel.GetText()}%1");
        }
    }
}

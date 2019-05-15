using ICSharpCode.AvalonEdit.Highlighting;
using System;
using System.IO;
using System.IO.Packaging;
using System.Linq;
using System.Printing;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Xps.Packaging;
using System.Windows.Xps.Serialization;
using System.Xml;
using Tonogram.Commands;
using Tonogram.Model;

namespace Tonogram.View
{
    public partial class MainWindow : Window
    {
        ShortcutKeyBindings keyBindings = new ShortcutKeyBindings();
        PopupToolController popupToolController;
        System.Windows.Forms.SaveFileDialog savePdfFileDialog;
        System.Windows.Forms.OpenFileDialog openFileDialog;
        System.Windows.Forms.SaveFileDialog saveFileDialog;
        IModelSource modelSource;

        public MainWindow()
        {
            InitDialogs();
            InitializeComponent();
            SetupModel();
            LoadAvalonHighlightTemplateFromAssembly("Tonogram.CustomHighlighting.xshd");

            AvalonEditor.TextArea.FontSize = 20;
            AvalonEditor.TextArea.PreviewKeyDown += TextArea_KeyDown;
            popupToolController = new PopupToolController(new CommandSource(AvalonEditor));

            keyBindings.AddShortCut(new KeyGesture(Key.S, ModifierKeys.Control), new LambdsCommand(() => Save()));
            AvalonEditor.TextArea.InputBindings.AddRange(keyBindings.GetKeyBindings());
        }

        public void Save()
        {
            var filename = AvalonEditor.Document.FileName;
            
            if (!File.Exists(filename))
            {
                if (saveFileDialog.ShowDialog() != System.Windows.Forms.DialogResult.OK)
                    return;
                filename = saveFileDialog.FileName;
            }
            AvalonEditor.Save(filename);
            AvalonEditor.Document.FileName = filename;
            AvalonEditor.Document.UndoStack.MarkAsOriginalFile();
        }

        public void Load()
        {
            if (openFileDialog.ShowDialog() != System.Windows.Forms.DialogResult.OK)
                return;
            AvalonEditor.Load(openFileDialog.FileName);
            AvalonEditor.Document.FileName = openFileDialog.FileName;
        }

        private void LoadAvalonHighlightTemplateFromAssembly(string templateFilename)
        {
            IHighlightingDefinition customHighlighting;
            using (Stream s = typeof(MainWindow).Assembly.GetManifestResourceStream(templateFilename))
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
        }

        private void InitDialogs()
        {
            savePdfFileDialog = new System.Windows.Forms.SaveFileDialog();
            savePdfFileDialog.FileName = "phonetic";
            savePdfFileDialog.Filter = "Pdf Files (*.pdf)|*.pdf";
            savePdfFileDialog.DefaultExt = "pdf";
            savePdfFileDialog.CheckFileExists = false;
            savePdfFileDialog.AddExtension = true;

            saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            saveFileDialog.FileName = "phonetic";
            saveFileDialog.Filter = "Text Files (*.txt)|*.txt";
            saveFileDialog.DefaultExt = "txt";
            saveFileDialog.CheckFileExists = false;
            saveFileDialog.AddExtension = true;

            openFileDialog = new System.Windows.Forms.OpenFileDialog();
            openFileDialog.Filter = "Text Files (*.txt)|*.txt";
            openFileDialog.DefaultExt = "txt";
            openFileDialog.CheckFileExists = true;
            openFileDialog.AddExtension = true;
        }

        private void SetupModel()
        {
            modelSource = new ModelSource(AvalonEditor);
            View.SetModel(modelSource);
        }

        private void TextArea_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.RightCtrl && !e.IsRepeat)
            {
                var popupPosition = AvalonEditor.TextArea.TextView.GetVisualPosition(
                    AvalonEditor.TextArea.Caret.Position,
                    ICSharpCode.AvalonEdit.Rendering.VisualYPosition.LineMiddle);

                popupPosition.Y -= AvalonEditor.VerticalOffset;
                var screenPoint = AvalonEditor.PointToScreen(popupPosition);
                popupToolController.Show(this, screenPoint);
            }
        }

        private void ExportPdfBtn_Click(object sender, RoutedEventArgs e)
        {
            if (!(savePdfFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK))
                return;

            //PageMediaSize pageMediaSize = new PageMediaSize(PageMediaSizeName.ISOA4);
            //PrintDialog printDialog = new PrintDialog();
            //PrintCapabilities capabilities = printDialog.PrintQueue.GetPrintCapabilities(printDialog.PrintTicket);

            PageConfiguration pageConfiguration = PageConfiguration.GetA4Page();
            var fixedDocumentCreator = new FixedDocumentCreator(pageConfiguration);
            var modelSource = new ModelSource(AvalonEditor);
            var renderables = modelSource.Get().Select(x => RenderItemFactory.Instance.Create(x)).Where(x => x != null).ToList();
            var newFixedDocument = fixedDocumentCreator.CreateFixedDocument(renderables);

            var fileName = savePdfFileDialog.FileName;
            var tempFile = Path.ChangeExtension(Path.Combine(Path.GetTempPath(), Path.GetRandomFileName()), "xps");

            try
            {
                File.Delete(fileName);
                using (Package container = Package.Open(tempFile, FileMode.Create))
                {
                    using (XpsDocument xpsDoc = new XpsDocument(container, CompressionOption.Maximum))
                    {
                        XpsSerializationManager rsm = new XpsSerializationManager(new XpsPackagingPolicy(xpsDoc), false);
                        rsm.SaveAsXaml(newFixedDocument);
                        //DocumentPaginator paginator = ((IDocumentPaginatorSource)document).DocumentPaginator;
                        // 8 inch x 6 inch, with half inch margin
                        //paginator = new DocumentPaginatorWrapper(paginator, new Size(768, 676), new Size(48, 48), DocumentTitle, DocumentFooter);
                    }
                }
                using (var stream = new FileStream(tempFile, FileMode.Open))
                {
                    var pdfXpsDoc = PdfSharp.Xps.XpsModel.XpsDocument.Open(stream);
                    PdfSharp.Xps.XpsConverter.Convert(pdfXpsDoc, fileName, 0);
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(this, ex.Message.Substring(0, Math.Min(100, ex.Message.Length)), "Error", MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.OK);
            }
            finally
            {
                File.Delete(tempFile);
            }
        }

        private void PrintBtn_Click(object sender, RoutedEventArgs e)
        {
            PrintDialog printDialog = new PrintDialog();
            PrintCapabilities capabilities = printDialog.PrintQueue.GetPrintCapabilities(printDialog.PrintTicket);
            PageConfiguration pageConfiguration = new PageConfiguration
            {
                OriginWidth = capabilities.PageImageableArea.OriginWidth,
                OriginHeight = capabilities.PageImageableArea.OriginHeight,
                ExtentWidth = capabilities.PageImageableArea.ExtentWidth,
                ExtentHeight = capabilities.PageImageableArea.ExtentHeight,
                PageWidth = printDialog.PrintableAreaWidth,
                PageHeight = printDialog.PrintableAreaHeight
            };
            FixedDocumentCreator fixedDocumentCreator = new FixedDocumentCreator(pageConfiguration);
            var modelSource = new ModelSource(AvalonEditor);
            var renderables = modelSource.Get().Select(x => RenderItemFactory.Instance.Create(x)).Where(x => x != null).ToList();
            var newFixedDocument = fixedDocumentCreator.CreateFixedDocument(renderables);

            Window wnd = new Window();
            DocumentViewer viewer = new DocumentViewer
            {
                Document = newFixedDocument
            };

            wnd.Content = viewer;
            wnd.ShowDialog();
        }

        private void ExportFileBtn_Click(object sender, RoutedEventArgs e)
        {
            Save();
        }

        private void ImportFileBtn_Click(object sender, RoutedEventArgs e)
        {
            Load();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (!AvalonEditor.Document.UndoStack.IsOriginalFile || (string.IsNullOrEmpty(AvalonEditor.Document.FileName) && AvalonEditor.Document.TextLength > 0))
            {
                if (MessageBox.Show(this, "Изменения не сохранены. Сохранить?", "Сохранение", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.Yes) == MessageBoxResult.Yes)
                {
                    Save();
                }
            }
        }

        private void UndoBtn_Click(object sender, RoutedEventArgs e)
        {
            if (AvalonEditor.CanUndo)
            {
                AvalonEditor.Undo();
            }
        }

        private void RedoBtn_Click(object sender, RoutedEventArgs e)
        {
            if (AvalonEditor.CanRedo)
            {
                AvalonEditor.Redo();
            }
        }
    }
}

using ICSharpCode.AvalonEdit.Document;
using ICSharpCode.AvalonEdit.Highlighting;
using RenderTest.Model;
using System;
using System.Collections;
using System.IO;
using System.IO.Packaging;
using System.Linq;
using System.Printing;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Xps;
using System.Windows.Xps.Packaging;
using System.Windows.Xps.Serialization;
using System.Xml;
using WpfText.Commands;
using WpfText.Model;
using WpfText.Pagination;

namespace WpfText.View
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

        //private void MyCommandCanExecute(object sender, CanExecuteRoutedEventArgs e)
        //{
        //    e.CanExecute = true;
        //}

        //private void MyCommandExecute(object sender, ExecutedRoutedEventArgs e)
        //{
        //    var sel = AvalonEditor.TextArea.Selection;

        //    var start = sel.StartPosition;
        //    var end = sel.EndPosition;

        //    int offsetStart = AvalonEditor.Document.GetOffset(start.Location);
        //    int offsetEnd = AvalonEditor.Document.GetOffset(end.Location);
        //    if (offsetStart > offsetEnd)
        //    {
        //        var tmp = offsetEnd;
        //        offsetEnd = offsetStart;
        //        offsetStart = tmp;
        //    }

        //    var text = AvalonEditor.Document.GetText(offsetStart, offsetEnd - offsetStart);
        //    sel.ReplaceSelectionWithText($"3%lf{sel.GetText()}%1");
        //}

        

        public void SaveAsXps(string fileName, DocumentPaginator paginator)
        {
            using (Package container = Package.Open(fileName + ".xps", FileMode.Create))
            {
                using (XpsDocument xpsDoc = new XpsDocument(container, CompressionOption.Maximum))
                {
                    XpsSerializationManager rsm = new XpsSerializationManager(new XpsPackagingPolicy(xpsDoc), false);
                    
                    //DocumentPaginator paginator = ((IDocumentPaginatorSource)document).DocumentPaginator;

                    // 8 inch x 6 inch, with half inch margin
                    //paginator = new DocumentPaginatorWrapper(paginator, new Size(768, 676), new Size(48, 48), DocumentTitle, DocumentFooter);

                    rsm.SaveAsXaml(paginator);
                }
            }

            Console.WriteLine("{0} generated.", fileName + ".xps");
        }

        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            PrintDialog printDialog = new PrintDialog();
            
            Size pageSize = new Size(printDialog.PrintableAreaWidth, printDialog.PrintableAreaHeight);
            Document doc = new Document();
            doc.PageSize = pageSize;
            var modelSource = new ModelSource(AvalonEditor);
            var renderables = modelSource.Get().Select(x => Factory.Instance.Create(x)).Where(x => x != null).ToList();

            var page = doc.CreatePage();
            var pageCanvas = page.PageCanvas;
            foreach (var item in renderables)
            {
                if (!pageCanvas.AddItem(item))
                {
                    page = doc.CreatePage();
                    pageCanvas = page.PageCanvas;
                    if (!pageCanvas.AddItem(item))
                    {
                        break;
                    }
                }
            }

            for (int i = 0; i < doc.PageCount; i++)
            {
                doc.GetPage(i).PageCanvas.InvalidateVisual();
            }
            DocumentPaginator paginator = new Paginator(doc);

           // SaveAsXps("out", paginator);
           //return;

            string tempFileName = "out.xps";

            //GetTempFileName creates a file, the XpsDocument throws an exception if the file already
            //exists, so delete it. Possible race condition if someone else calls GetTempFileName
            File.Delete(tempFileName);
            using (XpsDocument xpsDocument = new XpsDocument(tempFileName, FileAccess.ReadWrite))
            {
                XpsDocumentWriter writer = XpsDocument.CreateXpsDocumentWriter(xpsDocument);
                writer.Write(paginator);
                //PrintPreview previewWindow = new PrintPreview
                //{
                //    Owner = this,
                //    Document = xpsDocument.GetFixedDocumentSequence()
                //};
                //previewWindow.ShowDialog();
                xpsDocument.Close();
            }

            //if (printDialog.ShowDialog() == true)
            //{
            //    printDialog.PrintDocument(paginator, "Print demo");
            //}

            //RenderView canvas = new RenderView();
            //canvas.SetModel(new ModelSource(AvalonEditor));

            //Size pageSize = new Size(Printdlg.PrintableAreaWidth, Printdlg.PrintableAreaHeight);

            //// sizing of the element.

            //canvas.Measure(pageSize);

            //canvas.Arrange(new Rect(5, 5, pageSize.Width, pageSize.Height));

            //if ((bool)Printdlg.ShowDialog().GetValueOrDefault())
            //{

            //    Printdlg.PrintVisual(canvas, "Print Canvas");

            //}

            //canvas.Arrange(new Rect)
        }

        private void ExportPdfBtn_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.SaveFileDialog sd = new System.Windows.Forms.SaveFileDialog();
            sd.FileName = "phonetic";
            sd.DefaultExt = "pdf";
            sd.CheckFileExists = false;
            sd.AddExtension = true;
            if (!(sd.ShowDialog() == System.Windows.Forms.DialogResult.OK))
                return;

            PrintDialog printDialog = new PrintDialog();
            Size pageSize = new Size(printDialog.PrintableAreaWidth, printDialog.PrintableAreaHeight);
            Document document = new Document();
            document.PageSize = pageSize;
            var modelSource = new ModelSource(AvalonEditor);
            var renderables = modelSource.Get().Select(x => Factory.Instance.Create(x)).Where(x => x != null).ToList();

            var pageCanvas = document.CreatePage().PageCanvas;
            foreach (var item in renderables)
            {
                if (!pageCanvas.AddItem(item))
                {
                    pageCanvas = document.CreatePage().PageCanvas;
                    if (!pageCanvas.AddItem(item))
                    {
                        break;
                    }
                }
            }

            for (int i = 0; i < document.PageCount; i++)
                document.GetPage(i).PageCanvas.InvalidateVisual();

            FixedDocument fixedDoc = new FixedDocument();
            for (int i = 0; i < document.PageCount; i++)
            {
                var pp = document.GetPage(i);
                var canvas = pp.PageCanvas;

                PageContent pageContent = new PageContent();
                FixedPage page = new FixedPage();
                ((IAddChild)pageContent).AddChild(page);
                fixedDoc.Pages.Add(pageContent);
                page.Width = pageSize.Width;
                page.Height = pageSize.Height;

                canvas.Width = pageSize.Width;
                canvas.Height = pageSize.Height;

                page.Children.Add(canvas);
            }

            string fileName = sd.FileName;
            File.Delete(fileName);
            using (Package container = Package.Open(fileName + ".xps", FileMode.Create))
            {
                using (XpsDocument xpsDoc = new XpsDocument(container, CompressionOption.Maximum))
                {
                    XpsSerializationManager rsm = new XpsSerializationManager(new XpsPackagingPolicy(xpsDoc), false);

                    //DocumentPaginator paginator = ((IDocumentPaginatorSource)document).DocumentPaginator;

                    // 8 inch x 6 inch, with half inch margin
                    //paginator = new DocumentPaginatorWrapper(paginator, new Size(768, 676), new Size(48, 48), DocumentTitle, DocumentFooter);

                    rsm.SaveAsXaml(fixedDoc);
                }
            }
            using (var stream = new FileStream(fileName + ".xps", FileMode.Open))
            {
                var pdfXpsDoc = PdfSharp.Xps.XpsModel.XpsDocument.Open(stream);
                PdfSharp.Xps.XpsConverter.Convert(pdfXpsDoc, fileName, 0);
            }
            File.Delete(fileName + ".xps");

        }

        private void PrintBtn_Click(object sender, RoutedEventArgs e)
        {
            PageMediaSize pageSize = null;
            PrintDialog printDialog = new PrintDialog();
            pageSize = new PageMediaSize(PageMediaSizeName.ISOA4);
            printDialog.PrintTicket.PageMediaSize = pageSize;
            
           
            OpenPrintPreview(printDialog);
        }

        private void OpenPrintPreview(PrintDialog printDialog)
        {
            PrintCapabilities capabilities = printDialog.PrintQueue.GetPrintCapabilities(printDialog.PrintTicket);
            Size pageSize = new Size(printDialog.PrintableAreaWidth, printDialog.PrintableAreaHeight);
            Size visibleSize = new Size(capabilities.PageImageableArea.ExtentWidth, capabilities.PageImageableArea.ExtentHeight);

            Document document = new Document();
            document.PageSize = visibleSize;
            var modelSource = new ModelSource(AvalonEditor);
            var renderables = modelSource.Get().Select(x => Factory.Instance.Create(x)).Where(x => x != null).ToList();

            var pageCanvas = document.CreatePage().PageCanvas;
            foreach (var item in renderables)
            {
                if (!pageCanvas.AddItem(item))
                {
                    pageCanvas = document.CreatePage().PageCanvas;
                    if (!pageCanvas.AddItem(item))
                    {
                        break;
                    }
                }
            }

            for (int i = 0; i < document.PageCount; i++)
                document.GetPage(i).PageCanvas.InvalidateVisual();

            FixedDocument fixedDoc = new FixedDocument();
            for (int i = 0; i < document.PageCount; i++)
            {
                var pp = document.GetPage(i);
                var canvas = pp.PageCanvas;

                PageContent pageContent = new PageContent();
                FixedPage page = new FixedPage();
                ((IAddChild)pageContent).AddChild(page);
                fixedDoc.Pages.Add(pageContent);
                page.Width = pageSize.Width;
                page.Height = pageSize.Height;

                FixedPage.SetLeft(canvas, capabilities.PageImageableArea.OriginWidth);
                FixedPage.SetTop(canvas, capabilities.PageImageableArea.OriginHeight);

                canvas.Width = visibleSize.Width;
                canvas.Height = visibleSize.Height;

                page.Children.Add(canvas);
            }

            Window wnd = new Window();
            DocumentViewer viewer = new DocumentViewer();
            viewer.Document = fixedDoc;

            PrintServer myPrintServer = new PrintServer();
            PrintQueueCollection myPrintQueues = myPrintServer.GetPrintQueues();
            String printQueueNames = "My Print Queues:\n\n";
            foreach (PrintQueue pq in myPrintQueues)
            {
                printQueueNames += "\t" + pq.Name + "\n";
            }

            var q = LocalPrintServer.GetDefaultPrintQueue();
            var ut = q.UserPrintTicket;


            wnd.Content = viewer;
            wnd.ShowDialog();
        }
    }
}

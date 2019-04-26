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
        ShortcutKeyBindings keyBindings = new ShortcutKeyBindings();
        PopupToolController popupToolController;
        System.Windows.Forms.SaveFileDialog savePdfFileDialog = new System.Windows.Forms.SaveFileDialog();

        public EditorView()
        {
            savePdfFileDialog.FileName = "phonetic";
            savePdfFileDialog.Filter = "Pdf Files (*.pdf)|*.pdf";
            savePdfFileDialog.DefaultExt = "pdf";
            savePdfFileDialog.CheckFileExists = false;
            savePdfFileDialog.AddExtension = true;

            InitializeComponent();
            AvalonEditor.TextArea.FontSize = 20;
            AvalonEditor.TextArea.PreviewKeyDown += TextArea_KeyDown;

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

        private void ExportPdfBtn_Click(object sender, RoutedEventArgs e)
        {
            if (!(savePdfFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK))
                return;

            PrintDialog printDialog = new PrintDialog();
            PageMediaSize pageMediaSize = new PageMediaSize(PageMediaSizeName.ISOA4);
            printDialog.PrintTicket.PageMediaSize = pageMediaSize;
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
                        rsm.SaveAsXaml(fixedDoc);
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

        private void Test(object sender, RoutedEventArgs e)
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
        }

        private void TextArea_MouseDown(object sender, MouseButtonEventArgs e)
        {
            //AvalonEditor.Document.UndoStack.Undo();
            //DocumentLine line = AvalonEditor.Document.GetLineByOffset(AvalonEditor.CaretOffset);
            //AvalonEditor.Select(line.Offset, line.Length);
        }
    }
}

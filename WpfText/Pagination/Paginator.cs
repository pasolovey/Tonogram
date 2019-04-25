using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Documents;

namespace WpfText.Pagination
{
    public class Paginator : DocumentPaginator
    {
        private readonly Document doc;

        public Paginator(Document doc)
        {
            this.doc = doc;
        }

        public override bool IsPageCountValid => true;

        public override int PageCount => doc.PageCount;

        public override Size PageSize { get { return doc.PageSize; } set { } }

        public override IDocumentPaginatorSource Source
        {
            get
            {
                return null;
            }
        }

        public override DocumentPage GetPage(int pageNumber)
        {
            doc.CurrentPageIndex = pageNumber;
            var page = doc.CurrentPage;
            var printCanvas = page.PageCanvas;
            //Canvas printCanvas = new Canvas();
            //MainWindow mainWindow = (MainWindow)Application.Current.MainWindow;
            //mainWindow.Document.CurrentPageIndex = pageNumber;
            //mainWindow.DrawPage(printCanvas);
            //printCanvas.Measure(PageSize);
            //printCanvas.Arrange(new Rect(new Point(), PageSize));
            //printCanvas.UpdateLayout();
            var dc = new DocumentPage(printCanvas, doc.PageSize, new Rect(doc.PageSize), new Rect(doc.PageSize));
            return dc;
        }
    }
}

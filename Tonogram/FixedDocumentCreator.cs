using RenderTest.Render;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Markup;
using Tonogram.Pagination;

namespace Tonogram
{
    public sealed class FixedDocumentCreator
    {
        private PageConfiguration pageConfig;

        public FixedDocumentCreator(PageConfiguration pageConfig)
        {
            this.pageConfig = pageConfig;
        }

        public void SetPage(PageConfiguration pageConfig)
        {
            this.pageConfig = pageConfig;
        }

        public FixedDocument CreateFixedDocument(IEnumerable<IRenderable> renderables)
        {
            Size pageSize = new Size(pageConfig.PageWidth, pageConfig.PageHeight);
            Size visibleSize = new Size(pageConfig.ExtentWidth, pageConfig.ExtentHeight);

            Document document = new Document();
            document.PageSize = visibleSize;

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

                FixedPage.SetLeft(canvas, pageConfig.OriginWidth);
                FixedPage.SetTop(canvas, pageConfig.OriginHeight);

                canvas.Width = visibleSize.Width;
                canvas.Height = visibleSize.Height;

                page.Children.Add(canvas);
            }

            return fixedDoc;
        }
    }
}

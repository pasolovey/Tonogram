using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Tonogram.Pagination
{
    public class Document
    {
        private int currentPageIndex;

        private List<Page> pages = new List<Page>();

        public int PageCount => pages.Count;

        public Size PageSize { get; set; }

        public int CurrentPageIndex { get => currentPageIndex; set => currentPageIndex = value; }

        public Page CurrentPage => GetPage(currentPageIndex);

        public Page GetPage(int index)
        {
            if (index > pages.Count - 1)
                return null;
            return pages[index];
        }

        public Page CreatePage()
        {
            var page = new Page(PageSize);
            pages.Add(page);
            currentPageIndex = pages.Count - 1;
            return page;
        }
    }
    
}

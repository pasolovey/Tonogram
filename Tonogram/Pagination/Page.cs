using System.Windows;
using Tonogram.View;

namespace Tonogram.Pagination
{
    public class Page
    {
        PageCanvas pageCanvas;

        public Page()
        {
            pageCanvas = new PageCanvas();
        }

        public Page(Size pageSize)
        {
            pageCanvas = new PageCanvas();
            SetPageSize(pageSize);
        }

        public void SetPageSize(Size pageSize)
        {
            pageCanvas.Measure(pageSize);
            pageCanvas.Arrange(new Rect(pageSize));
            pageCanvas.UpdateLayout();
        }

        public PageCanvas PageCanvas => pageCanvas;
    }
    
}

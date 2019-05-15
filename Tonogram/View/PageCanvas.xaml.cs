using RenderTest.Render;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Tonogram.View
{
    public partial class PageCanvas : Canvas
    {
        private List<IRenderable> renderables = new List<IRenderable>();
        private Point measureStart = new Point(0, 0);

        public PageCanvas()
        {
            InitializeComponent();
        }

        public bool AddItem(IRenderable item)
        {
            if (measureStart.Y + item.Height > ActualHeight)
                return false;
            if (measureStart.X + item.Width <= this.ActualWidth)
            {
                measureStart.X += item.Width;
                renderables.Add(item);
                return true;
            }
            measureStart.X = 0;
            measureStart.Y += item.Height;
            if (measureStart.Y + item.Height > ActualHeight)
                return false;
            measureStart.X += item.Width;
            renderables.Add(item);
            return true;
        }

        protected override void OnRender(DrawingContext dc)
        {
            Render(dc);
        }

        private void Render(DrawingContext dc)
        {
            if (renderables == null)
                return;
            Point start = new Point(0, 0);
            foreach (var item in renderables)
            {
                if (start.X + item.Width > ActualWidth)
                {
                    start.X = 0;
                    start.Y += item.Height;
                }
                item.OnDraw(dc, start);
                start.X = start.X + item.Width;
            }
        }
    }
}

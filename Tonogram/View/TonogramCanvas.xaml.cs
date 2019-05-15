using RenderTest.Render;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Xps.Packaging;
using Tonogram.Model;

namespace Tonogram.View
{
    /// <summary>
    /// Логика взаимодействия для RenderView.xaml
    /// </summary>
    public partial class TonogramCanvas : Canvas
    {
        private IModelSource modelSource;
        private IEnumerable<IRenderable> renderables;

        static TonogramCanvas()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(TonogramCanvas), new FrameworkPropertyMetadata(typeof(TonogramCanvas)));
        }

        public TonogramCanvas()
        {
            InitializeComponent();
        }

        public void SetModel(IModelSource modelSource)
        {
            this.modelSource = modelSource;
            renderables = modelSource.Get()?.Select(x => RenderItemFactory.Instance.Create(x)).Where(x => x != null).ToList();
            this.modelSource.ModelChanged += ModelSource_ModelChanged;
        }

        private void ModelSource_ModelChanged(object sender, EventArgs e)
        {
            renderables = modelSource.Get()?.Select(x => RenderItemFactory.Instance.Create(x)).Where(x => x != null).ToList();
            InvalidateMeasure();
            InvalidateVisual();
        }

        protected override void OnRender(DrawingContext dc)
        {
            if (modelSource == null)
                return;
            Render(dc);
        }

        protected override Size MeasureOverride(Size constraint)
        {
            double bottomMost = 0d;
            double rightMost = ActualWidth;
            if (Parent is FrameworkElement fe)
            {
                rightMost = fe.ActualWidth;
            }

            if (renderables == null)
                return new Size(rightMost, bottomMost);
            Point start = new Point(0, 0);
            foreach (var item in renderables)
            {
                if (bottomMost == 0d)
                    bottomMost += item.Height;
                if (start.X + item.Width > ActualWidth)
                {
                    bottomMost += item.Height;
                    start.X = 0;
                    start.Y += item.Height;
                }
                start.X = start.X + item.Width;
            }

            return new Size(rightMost, bottomMost);
        }

        protected override void OnPreviewMouseRightButtonUp(MouseButtonEventArgs e)
        {
            //XpsDocument doc = new XpsDocument(@".\canvas-10cm.xps", System.IO.FileAccess.Write);
            //XpsDocumentWriter writer = XpsDocument.CreateXpsDocumentWriter(doc);
            //writer.Write(this);
            //doc.Close();
        }

        private void Render(DrawingContext dc)
        {
            if (renderables == null)
                return;
            Point start = new Point(0, 0);
            foreach (var item in renderables)
            {
                if (start.X + item.Width > this.ActualWidth)
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

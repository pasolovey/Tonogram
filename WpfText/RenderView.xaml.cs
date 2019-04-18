using RenderTest.Model;
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

namespace WpfText
{
    /// <summary>
    /// Логика взаимодействия для RenderView.xaml
    /// </summary>
    public partial class RenderView : Canvas
    {
        public RenderView()
        {
            InitializeComponent();
        }

        private Pen DefaultPen = new Pen(Brushes.Black, 1);
        private IModelSource modelSource;


        public void SetModel(IModelSource modelSource)
        {
            this.modelSource = modelSource;
            this.modelSource.ModelChanged += ModelSource_ModelChanged;
        }

        private void ModelSource_ModelChanged(object sender, EventArgs e)
        {
            InvalidateVisual();
        }

        protected override void OnRender(DrawingContext dc)
        {
            base.OnRender(dc);
            if (modelSource == null)
                return;
            var models = modelSource.Get();
            var drawItems = models.Select(x => Factory.Instance.Create(x)).Where(x => x != null).ToList();
            Render(dc, drawItems);
        }

        protected override void OnPreviewMouseRightButtonUp(MouseButtonEventArgs e)
        {
            //XpsDocument doc = new XpsDocument(@".\canvas-10cm.xps", System.IO.FileAccess.Write);
            //XpsDocumentWriter writer = XpsDocument.CreateXpsDocumentWriter(doc);
            //writer.Write(this);
            //doc.Close();
        }

        void Render(DrawingContext dc, IEnumerable<IRenderable> renderables)
        {
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

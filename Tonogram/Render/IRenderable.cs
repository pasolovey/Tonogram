using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace RenderTest.Render
{
    public interface IRenderable
    {
        void OnDraw(DrawingContext dc, Point position);
        double Width { get; }
        double Height { get; }
    }
}

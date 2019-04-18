using RenderTest.Render;
using System;
using WpfText.Render;

namespace RenderTest.Model
{
    public class Factory
    {
        public static Factory Instance { get; }
        Random rnd = new Random(Environment.TickCount);


        static Factory()
        {
            Instance = new Factory();
        }

        public IRenderable Create(ModelItem item)
        {
            IRenderable res = null;
            if (item.Type == 1)
            {
                res = new StresPhoneticShape(item.Text) { StartLevel = item.Start, StopLevel = item.End };
            }
            else if (item.Type == 2)
            {
                res = new NoStresPhoneticShape(item.Text) { StartLevel = item.Start, StopLevel = item.End };
            }
            else if (item.Type == 3)
            {
                res = new MonotonicBezierPhoneticShape(item.Text) { StartLevel = item.Start, StopLevel = item.End };
            }

            return res;
        }
    }
}

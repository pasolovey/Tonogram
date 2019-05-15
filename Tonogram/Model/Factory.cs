using RenderTest.Render;
using System.Collections.Generic;
using Tonogram.Render;

namespace Tonogram.Model
{
    public class RenderItemFactory
    {
        public static RenderItemFactory Instance { get; }

        static RenderItemFactory()
        {
            Instance = new RenderItemFactory();
        }

        public IEnumerable<IRenderable> Create(IEnumerable<ModelItem> items)
        {
            foreach (var item in items)
            {
                var renderable = Create(item);
                if (renderable != null)
                    yield return renderable;
            }
        }

        public IRenderable Create(ModelItem item)
        {
            IRenderable res = null;
            if (item.Type == 1)
            {
                res = new StressPhoneticShape(item.Text) { StartLevel = item.Start, StopLevel = item.End };
            }
            else if (item.Type == 2)
            {
                res = new NoStressPhoneticShape(item.Text) { StartLevel = item.Start, StopLevel = item.End };
            }
            else if (item.Type == 3)
            {
                res = new TwoPointBezierPhoneticShape(item.Text) { StartLevel = item.Start, StopLevel = item.End };
            }

            else if (item.Type == 4)
            {
                var tmp = new PausePhoneticShape(item.Text);
                if (item is IPausable ps)
                {
                    tmp.Count = ps.PauseCount;
                }
                res = tmp;
            }
            else if(item.Type == 5)
            {
                res = new BreathPhoneticShape(item.Text);
            }
            else if (item.Type == 6)
            {
                if (item is IMidPoint mp)
                {
                    res = new ThreePointBezierPhoneticShape(item.Text) { StartLevel = item.Start, StopLevel = item.End, MidLevel = mp.Level};
                }
            }
            else if (item.Type == 7)
            {
                res = new SpecialRisePhoneticShape(item.Text);
            }

            return res;
        }
    }
}

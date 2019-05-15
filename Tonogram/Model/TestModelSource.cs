using System;
using System.Collections.Generic;
using System.Linq;

namespace Tonogram.Model
{
    public class TestModelSource : IModelSource
    {
        public event EventHandler ModelChanged;

        public IEnumerable<ModelItem> Get()
        {
            Random rnd = new Random(System.Environment.TickCount);
            string text = @"But the thing is I do not know the size of my canvas until runtime But the
thing is I do not know the size of my canvas until runtime But the thing is I do not know the size of my 
canvas until runtime But the thing is I do not know the size of my canvas until runtime But the thing is
I do not know the size of my canvas until runtime
But the thing is I do not know the size of my canvas until runtime But the
thing is I do not know the size of my canvas until runtime But the thing is I do not know the size of my 
canvas until runtime But the thing is I do not know the size of my canvas until runtime But the thing is
I do not know the size of my canvas until runtime";
            var words = text.Trim().Replace(Environment.NewLine, "").Split(new[] { " " }, StringSplitOptions.RemoveEmptyEntries);
            return words.Select(x => new ModelItem() { Text = x, Type = rnd.Next(0, 3) });
        }
    }
}

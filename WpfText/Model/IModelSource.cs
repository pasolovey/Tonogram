using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RenderTest.Model
{
    public interface IModelSource
    {
        IEnumerable<ModelItem> Get();
        event EventHandler ModelChanged;
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Tonogram.Model
{
    public interface IModelSource
    {
        IEnumerable<ModelItem> Get();
        event EventHandler ModelChanged;
    }
}

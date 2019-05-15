using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Tonogram.Commands;

namespace Tonogram
{
    public class ShortcutKeyBindings
    {
        public List<InputBinding> Bindings { get; set; } = new List<InputBinding>();

        public ShortcutKeyBindings()
        {
        
        }

        public void AddShortCut(KeyGesture kg, ICommand command)
        {
            InputBinding ib = new InputBinding(command, kg);
            Bindings.Add(ib);
        }

        public ICollection GetKeyBindings()
        {
            return Bindings;
        }
    }
}

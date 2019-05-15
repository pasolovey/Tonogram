using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tonogram.Commands
{

    public abstract class BaseToolCommand : IToolCommand
    {
        public abstract string Name { get; }
        protected object tooltip = null;

        public object Tooltip => tooltip;

        public event EventHandler CanExecuteChanged;

        public virtual bool CanExecute(object parameter)
        {
            return true;
        }

        public abstract void Execute(object parameter);
    }
}

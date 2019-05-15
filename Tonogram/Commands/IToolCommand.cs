using System.Windows.Input;

namespace Tonogram.Commands
{
    public interface IToolCommand : ICommand
    {
        string Name { get; }
        object Tooltip { get; }
    }
}

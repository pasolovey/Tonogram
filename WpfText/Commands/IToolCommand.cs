using System.Windows.Input;

namespace WpfText.Commands
{
    public interface IToolCommand : ICommand
    {
        string Name { get; }
    }
}

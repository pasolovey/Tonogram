using System;
using System.Windows;
using Tonogram.Commands;
using Tonogram.View;

namespace Tonogram
{
    internal class PopupToolController
    {
        CommandWindow instance;

        public CommandSource CommandSource { get; }

        public PopupToolController(CommandSource commandSource)
        {
            CommandSource = commandSource;
        }

        public void Show(Window parent, Point p)
        {
            Console.WriteLine("show");
            if (instance == null)
            {
                instance = Create(parent);
            }

            if (instance != null)
            {
                instance.Left = p.X;
                instance.Top = p.Y + instance.Height / 2;
                instance.Show();
                instance.Focus();
            }
        }

        public void Hide()
        {
            Console.WriteLine("hide");
            if (instance != null)
            {
                instance.Close();
                instance = null;
            }
        }

        CommandWindow Create(Window parent)
        {
            var newWindow = new CommandWindow();
            newWindow.SetCommands(CommandSource.GetToolCommands());
            newWindow.PressTool += NewWindow_PressTool;
            newWindow.Closed += NewWindow_Closed;
            newWindow.Owner = parent;
            newWindow.ShowInTaskbar = false;
            
            return newWindow;
        }

        private void NewWindow_Closed(object sender, EventArgs e)
        {
            instance = null;
        }

        private void NewWindow_PressTool(object sender, EventArgs e)
        {
            Hide();
        }
    }
}

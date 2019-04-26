using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using WpfText.Commands;

namespace WpfText.View
{
    /// <summary>
    /// Логика взаимодействия для CommandWindow.xaml
    /// </summary>
    public partial class CommandWindow : Window
    {
        public event EventHandler PressTool;

        public CommandWindow()
        {
            InitializeComponent();
            Loaded += CommandWindow_Loaded;    
        }

        private void CommandWindow_Loaded(object sender, RoutedEventArgs e)
        {
            if (Stack.Children.Count > 0)
                Stack.Children[0].Focus();
        }

        protected override void OnPreviewKeyDown(KeyEventArgs e)
        {
            if (e.Key == Key.Escape || e.Key == Key.RightCtrl)
                Close();
            else
                base.OnPreviewKeyDown(e);
        }

        public void SetCommands(IEnumerable<IToolCommand> commands)
        {
            foreach (var command in commands)
            {
                var btn = new Button
                {
                    Content = command.Name,
                    ToolTip = command.Tooltip
                };
                int tabIndex = 0;
                btn.Click += (s, e) =>
                {
                    Console.WriteLine(command.GetType());
                    command.Execute(null);
                    OnPressTool();
                };
                btn.Focusable = true;
                btn.TabIndex = tabIndex++;
                Stack.Children.Add(btn);
            }
        }

        private void OnPressTool()
        {
            PressTool?.Invoke(this, EventArgs.Empty);
        }
    }
}

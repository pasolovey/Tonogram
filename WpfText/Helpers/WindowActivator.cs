using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WpfText.Components;

namespace WpfText.Helpers
{
    public class WindowActivator<T>
        where T : Window
    {
        static T instance = null;

        public static ICloser Show(double x, double y, Func<T> factory)
        {
            var instance = factory();
            instance.Left = x;
            instance.Top = y;
            instance.Show();
            return new WindowCloser<T>(instance);
        }
    }

    public interface ICloser : IDisposable
    {
        void Close();
    }


    public class WindowCloser<T> : ICloser
        where T : Window
    {
        private T window;

        public WindowCloser(T window)
        {
            this.window = window;
        }

        public void Close()
        {
            if (window != null)
            {
                window.Close();
            }
        }

        public void Dispose()
        {
            Close();
        }
    }

}

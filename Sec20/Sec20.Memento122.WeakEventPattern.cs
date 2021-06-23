using Autofac;
using Autofac.Core.Activators;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using static System.Console;

namespace Sec20.Memento122
{
    public class Button
    {
        public event EventHandler Clicked;
        public void Fire()
        {
            Clicked?.Invoke(this, EventArgs.Empty);
        }
    }
    public class Window
    {
        public Window(Button button)
        {
            //button.Clicked += ButtonOnClicked;
            WeakEventManager<Button, EventArgs>
                .AddHandler(button, "Clicked", ButtonOnClicked);
        }

        private void ButtonOnClicked(object sender, EventArgs e)
        {
            WriteLine("Button clicked (window handler)");
        }
        ~Window()
        {
            WriteLine("window finalized");
        }
    }
    class Demo
    {
        //static void Main(string[] args)
        //{
        //    main();
        //    ReadLine();
        //}
        static void main()
        {
            var button = new Button();
            var window = new Window(button);
            var windowRef = new WeakReference(window);
            button.Fire();
            WriteLine("Setting window to null");
            window = null;

            FireGC();
            WriteLine($"Is the window alive after GC? {windowRef.IsAlive}");
        }
        private static void FireGC()
        {
            WriteLine("Starting GC");
            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();
            WriteLine("GC is done");
        }
    }

}
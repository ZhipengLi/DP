using Autofac;
using Autofac.Features.Metadata;
using MoreLinq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace Sec07.Vector46
{
    public interface ICommand
    {
        void Execute();
    }

    class SaveCommand : ICommand
    {
        public void Execute()
        {
            WriteLine("Saving a file");
        }
    }
    class OpenCommand : ICommand
    {
        public void Execute()
        {
            WriteLine("Opening a file");
        }
    }
    public class Button
    {
        private ICommand command;
        private string name;
        public Button(ICommand command, string name)
        {
            if (command == null)
            {
                throw new ArgumentNullException(paramName: nameof(command));
            }
            this.command = command;
            this.name = name;
        }
        public void Click()
        {
            command.Execute();
        }
        public void PrintMe()
        {
            WriteLine($"I amd a button called {name}");
        }
    }
    public class Editor
    {
        private IEnumerable<Button> buttons;
        public IEnumerable<Button> Buttons => buttons;
        public Editor(IEnumerable<Button> buttons)
        {
            if (buttons == null)
            {
                throw new ArgumentNullException(paramName: nameof(buttons));
            }
            this.buttons = buttons;
        }
        public void ClickAll()
        {
            foreach (var btn in buttons)
            {
                btn.Click();
            }
        }
    }
    class Demo
    {
        static void Main(string[] args)
        {
            var b = new ContainerBuilder();
            b.RegisterType<SaveCommand>().As<ICommand>()
                .WithMetadata("Name", "Save");
            b.RegisterType<OpenCommand>().As<ICommand>()
                .WithMetadata("Name", "Open");
            //b.RegisterType<Button>();
            //b.RegisterAdapter<ICommand, Button>(cmd => new Button(cmd));
            b.RegisterAdapter<Meta<ICommand>, Button>(
                cmd => new Button(cmd.Value, (string)cmd.Metadata["Name"]));
            b.RegisterType<Editor>();
            using (var c = b.Build())
            {
                var editor = c.Resolve<Editor>();
                foreach (var btn in editor.Buttons)
                    btn.PrintMe();
            }
            ReadLine();
        }
    }
}

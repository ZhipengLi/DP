using Autofac;
using Autofac.Features.Metadata;
using Dynamitey;
using ImpromptuInterface;
using MoreLinq;
using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Dynamic;
using System.Linq;
using System.Net.Cache;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace Sec12.Proxy81
{
    // mvvm
    // model
    public class Person
    {
        public string FirstName, LastName;

        //
    }

    // view = ui

    // viewmodel
    public class PersonViewModel
        :INotifyPropertyChanged
    {
        private readonly Person person;
        public PersonViewModel(Person person)
        {
            this.person = person;
        }
        public string FirstName
        {
            get => person.FirstName;
            set
            {
                if (person.FirstName == value)
                    return;
                person.FirstName = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(FullName));
            }
        }

        public string LastName
        {
            get => person.LastName;
            set
            {
                if (person.LastName == value)
                    return;
                person.LastName = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(FullName));
            }
        }
        public string FullName
        {
            get => $"{FirstName} {LastName}".Trim();
            set
            {
                if (value == null)
                {
                    FirstName = LastName = null;
                    return;
                }
                var items = value.Split();
                if (items.Length > 0)
                    FirstName = items[0];
                if (items.Length > 1)
                    LastName = items[1];
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(
            [CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this,
              new PropertyChangedEventArgs(propertyName));
        }
    }
    //==============================================================================================
    class Demo
    {
        static void Main(string[] args)
        {
            main();
            ReadLine();
        }
        static void main()
        {
            var p = new Person() { FirstName = "first", LastName = "last" };
            var vm = new PersonViewModel(p);
            vm.PropertyChanged += (sender, args) => 
            {
                var propertyValue = sender.GetType().GetProperty(args.PropertyName).GetValue(sender);
                WriteLine($"{args.PropertyName} has changed to {propertyValue}."); 
            };

            vm.FullName = "first1 last1";
        }
    }

    //============================================================================================
    [TestFixture]
    public class Tests
    {
        [Test]
        public void BasicTest()
        {
            StringBuilder builder = new StringBuilder();

            var p = new Person() { FirstName = "first", LastName = "last" };
            var vm = new PersonViewModel(p);
            vm.PropertyChanged += (sender, args) =>
            {
                var propertyValue = sender.GetType().GetProperty(args.PropertyName).GetValue(sender);
                builder.AppendLine($"{args.PropertyName} has changed to {propertyValue}.");
            };

            vm.FullName = "first1 last1";
            string res = builder.ToString();
            Assert.IsTrue(res.Contains("FirstName has changed to first1."));
            Assert.IsTrue(res.Contains("FullName has changed to first1 last."));
            Assert.IsTrue(res.Contains("LastName has changed to last1."));
            Assert.IsTrue(res.Contains("FullName has changed to first1 last1."));
        }
    }
}


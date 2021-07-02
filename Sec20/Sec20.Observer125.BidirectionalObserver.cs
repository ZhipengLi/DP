using Autofac;
using Autofac.Core.Activators;
using Sec20.Memento121;
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
using System.Reactive.Linq;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Reflection;

namespace Sec20.Observer125
{
    public class Product : INotifyPropertyChanged
    {
        private string name;
        public string Name 
        {
            get => name;
            set
            {
                if (value == name) return;
                name = value;
                OnPropertyChanged("Name");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public override string ToString()
        {
            return $"Product : {Name}";
        }
    }
    public class Window : INotifyPropertyChanged
    {
        private string productName;
        public string ProductName
        {
            get => productName;
            set
            {
                if (value == productName) return;
                productName = value;
                OnPropertyChanged("ProductName");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public override string ToString()
        {
            return $"Window : {ProductName}";
        }
    }
    public sealed class BidirectionalBinding : IDisposable
    {
        private bool disposed;
        // first second
        // firstProp, secondProp
        public BidirectionalBinding(
            INotifyPropertyChanged first,
            Expression<Func<object>> firstProperty, // ()=>x.Foo
            INotifyPropertyChanged second,
            Expression<Func<object>> secondProperty // ()=>x.Foo
            )
        {
            // xxxProperty ix MemberExpression
            // Member of the above has PropertyInfo
            if (firstProperty.Body is MemberExpression firstExpr
                && secondProperty.Body is MemberExpression secondExpr)
            {
                if (firstExpr.Member is PropertyInfo firstProp &&
                    secondExpr.Member is PropertyInfo secondProp)
                {
                    first.PropertyChanged += (sender, args) => 
                    {
                        if (!disposed)
                            secondProp.SetValue(second, firstProp.GetValue(first));
                    };
                    second.PropertyChanged += (sender, args) =>
                    {
                        if (!disposed)
                            firstProp.SetValue(first, secondProp.GetValue(second));
                    };
                }
            }

        }
        public void Dispose()
        {
            disposed = true;
        }
    }
    public class Demo
    {
        static void Main(string[] args)
        {
            main();
            ReadLine();
        }
        static void main()
        {
            var product = new Product { Name = "Book" };
            var window = new Window { ProductName = "Book" };

            //product.PropertyChanged += (sender, eventArgs) => {
            //    if (eventArgs.PropertyName == "Name")
            //    {
            //        WriteLine("Name changed in Product");
            //        window.ProductName = product.Name;
            //    }
            //};
            //window.PropertyChanged += (sender, eventArgs) =>
            //{
            //    if (eventArgs.PropertyName == "ProductName")
            //    {
            //        WriteLine("ProductName changed in Window");
            //        product.Name = window.ProductName;
            //    }
            //};

            using (var binding = new BidirectionalBinding(
                    product,
                    () => product.Name,
                    window,
                    () => window.ProductName
                ))
            {
                product.Name = "Smart Book";
                window.ProductName = "Really smart book";

                WriteLine(product);
                WriteLine(window);
            }

        }
    }

}
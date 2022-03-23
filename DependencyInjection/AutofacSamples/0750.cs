using Autofac;
using Autofac.Configuration;
using Autofac.Core;
using Autofac.Core.Activators.Delegate;
using Autofac.Core.Lifetime;
using Autofac.Core.Registration;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AutofacSamples0750
{
    public class ParentWithProperty
    {
        public ChildWithProperty Child { get; set; }
        public override string ToString()
        {
            return "Parent";
        }
    }

    public class ChildWithProperty
    {
        public ParentWithProperty Parent { get; set; }
        public override string ToString()
        {
            return "Child";
        }
    }
    public class ParentWithConstructor1
    {
        public ChildWithProperty1 Child;
        public ParentWithConstructor1(ChildWithProperty1 child)
        {
            this.Child = child;
        }
        public override string ToString()
        {
            return "Parent with a ChildWithProperty";
        }
    }
    public class ChildWithProperty1
    {
        public ParentWithConstructor1 Parent { get; set; }
        public override string ToString()
        {
            return "Child";
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            var b = new ContainerBuilder();
            b.RegisterType<ParentWithConstructor1>().InstancePerLifetimeScope();
            b.RegisterType<ChildWithProperty1>()
                .InstancePerLifetimeScope()
                .PropertiesAutowired(PropertyWiringOptions.AllowCircularDependencies);
            using (var c = b.Build())
            {
                Console.WriteLine(c.Resolve<ParentWithConstructor1>().Child.Parent);

            }

                //Console.WriteLine(container.Resolve<Child>().Parent);
                Console.ReadLine();
        }
        static void Main_(string[] args)
        {
            var b = new ContainerBuilder();
            b.RegisterType<ParentWithProperty>()
                .InstancePerLifetimeScope()
                .PropertiesAutowired(PropertyWiringOptions.AllowCircularDependencies);
            b.RegisterType<ChildWithProperty>()
                .InstancePerLifetimeScope()
                .PropertiesAutowired(PropertyWiringOptions.AllowCircularDependencies);

            using (var c = b.Build())
            {
                Console.WriteLine(c.Resolve<ParentWithProperty>().Child);
            }

                //Console.WriteLine(container.Resolve<Child>().Parent);
                Console.ReadLine();
        }
    }
}

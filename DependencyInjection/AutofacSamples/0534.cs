using Autofac;
using Autofac.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AutofacSamples0534
{
    public interface IResource
    { 
    
    }
    public class SingletonResource : IResource 
    {
        public SingletonResource()
        {
            Console.WriteLine("SingletonResource created");
        }
        public void Dispose()
        {
            Console.WriteLine("SingletonResource destroyed");
        }
    }
    public class InstancePerDependencyResource : IResource, IDisposable
    {
        public InstancePerDependencyResource()
        {
            Console.WriteLine("InstancePerDependencyResource per dep created");
        }
        public void Dispose()
        {
            Console.WriteLine("InstancePerDependencyResource per dep destroyed");
        }
    }
    public class ResourceManager
    {
        public ResourceManager(IEnumerable<IResource> resources)
        {
            if (resources == null)
            {
                throw new ArgumentNullException(nameof(resources));
            }
            Resources = resources;
        }
        public IEnumerable<IResource> Resources { get; set; }
    }
    class Program
    {
        static void Main0534(string[] args)
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<ResourceManager>().SingleInstance();
            builder.RegisterType<SingletonResource>().As<IResource>().SingleInstance();
            builder.RegisterType<InstancePerDependencyResource>().As<IResource>();

            using (var container = builder.Build())
            using (var scope = container.BeginLifetimeScope())
            {
                scope.Resolve <ResourceManager> ();
            }

                Console.ReadLine();
        }
    }
}

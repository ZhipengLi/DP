using Autofac;
using Autofac.Configuration;
using Autofac.Core;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AutofacSamples0642
{
    public interface IOperation
    {
        float Calculate(float a, float b);
    }
    public class Addition : IOperation
    {
        public float Calculate(float a, float b)
        {
            return a + b;
        }
    }
    public class Multiplilcation : IOperation
    {
        public float Calculate(float a, float b)
        {
            return a * b;
        }
    }
    class Program
    {
        static void Main0642(string[] args)
        {
            var configBuilder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("config0642.json");
            var configuration = configBuilder.Build();

            var containerBuilder = new ContainerBuilder();
            var configModule = new ConfigurationModule(configuration);
            containerBuilder.RegisterModule(configModule);

            using (var container = containerBuilder.Build())
            {
                float a = 3, b = 4;
                foreach (IOperation op in container.Resolve<IList<IOperation>>())
                {
                    Console.WriteLine($"{op.GetType().Name} of {a} and {b} = {op.Calculate(a, b)}");
                }
            }

            //Console.WriteLine(container.Resolve<Child>().Parent);
            Console.ReadLine();
        }
    }
}

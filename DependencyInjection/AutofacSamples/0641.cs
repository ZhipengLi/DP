using Autofac;
using Autofac.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AutofacSamples0641
{
    public interface IVehicle
    {
        void Go();
    }
    class Truck : IVehicle
    {
        private IDriver driver;
        public Truck(IDriver driver)
        {
            this.driver = driver;
        }
        public void Go()
        {
            driver.Drive();

        }
    }
    public interface IDriver
    {
        void Drive();
    }
    public class CrazyDriver : IDriver
    {
        public void Drive()
        {
            Console.WriteLine("Going too fast and crashing into a tree");
        }
    }
    public class SaneDriver : IDriver
    {
        public void Drive()
        {
            Console.WriteLine("Drive safely to destination");
        }
    }

    public class TransportModule : Autofac.Module
    {
        public bool ObeySpeedLimit { get; set; }
        protected override void Load(ContainerBuilder builder)
        {
            if (ObeySpeedLimit)
            {
                builder.RegisterType<SaneDriver>().As<IDriver>();
            }
            else
            {
                builder.RegisterType<CrazyDriver>().As<IDriver>();
            }
            builder.RegisterType<Truck>().As<IVehicle>();
          
        }
    }
    class Program
    {
        static void Main0641(string[] args)
        {
            var builder = new ContainerBuilder();
            builder.RegisterModule(new TransportModule { ObeySpeedLimit = true});
            using (var c = builder.Build())
            {
                c.Resolve<IVehicle>().Go();
            }

                //Console.WriteLine(container.Resolve<Child>().Parent);
                Console.ReadLine();
        }
    }
}

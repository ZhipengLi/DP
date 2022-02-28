using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutofacSamples0207
{
    public interface ILog
    {
        void Write(string message);
    }

    public class ConsoleLog : ILog
    {
        public void Write(string message)
        {
            Console.WriteLine(message);
        }
    }
    public class Engine
    {
        private ILog log;
        private int id;
        public Engine(ILog log)
        {
            this.log = log;
            id = new Random().Next();
        }
        public void Ahead(int power)
        {
            log.Write($"Engine [{id}] ahead {power}");
        }
    }
    public class Car
    {
        private Engine engine;
        private ILog log;
        public Car(Engine engine, ILog log)
        {
            this.engine = engine;
            this.log = log;
        }
        public void Go()
        {
            engine.Ahead(100);
            log.Write("Car going forward...");
        }
    }
    class Program
    {
        static void Main0207(string[] args)
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<ConsoleLog>().As<ILog>().AsSelf();
            builder.RegisterType<Engine>();
            builder.RegisterType<Car>();
            IContainer container = builder.Build();
            var car = container.Resolve<Car>();
            car.Go();
            var consoleLog = container.Resolve<ConsoleLog>();
            Console.ReadLine();
        }
    }
}

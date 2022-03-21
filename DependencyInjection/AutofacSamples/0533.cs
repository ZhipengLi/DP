﻿using Autofac;
using Autofac.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AutofacSamples0533
{
    public interface ILog
    {
        void Write(string message);
    }
    public interface IConsole { }

    public class ConsoleLog : ILog, IConsole
    {
        public ConsoleLog()
        {
            Console.WriteLine("Constructing a console log");
        }
        public void Write(string message)
        {
            Console.WriteLine(message);
        }
    }
    public class EmailLog : ILog
    {
        private const string adminEmail = "admin@foo.com";

        public void Write(string message)
        {
            Console.WriteLine($"Email sent to {adminEmail} : {message}");
        }
    }
    public class Engine
    {
        private ILog log;
        private int id;
        public Engine(ILog log, int id)
        {
            this.log = log;
            this.id = id;// new Random().Next();
        }
        public void Ahead(int power)
        {
            log.Write($"Engine [{id}] ahead {power}");
        }
    }

    public class SMSLog : ILog
    {
        private string phoneNumber;
        public SMSLog(string phoneNumber)
        {
            this.phoneNumber = phoneNumber;
        }
        public void Write(string message)
        {
            Console.WriteLine($"SMS to {phoneNumber} : {message}");
        }
    }
    public class Car
    {
        private Engine engine;
        private ILog log;
        public Car(Engine engine)
        {
            this.engine = engine;
            this.log = new EmailLog();
        }
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

    public class Parent
    {
        public override string ToString()
        {
            return "I am your father";
        }
    }
    public class Child
    {
        public string Name { get; set; }
        public Parent Parent { get; set; }

        public void SetParent(Parent parent)
        {
            this.Parent = parent;
        }
    }
    public class ParentChildModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<Parent>();
            builder.Register(c => new Child() { Parent = c.Resolve<Parent>() });
        }
    }
    class Program
    {
        static void Main0533(string[] args)
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<ConsoleLog>()//.As<ILog>()
                .InstancePerMatchingLifetimeScope("foo")
                //.InstancePerLifetimeScope()
                ;//.SingleInstance();

            var container = builder.Build();

            //using (var scope = container.BeginLifetimeScope())
            //{
            //    var log = scope.Resolve<ILog>();
            //    log.Write("Testing!");

            //    scope.Resolve<ILog>();
            //}

            using (var scope1 = container.BeginLifetimeScope("foo"))
            {
                for (int i = 0; i < 3; i++)
                {
                    scope1.Resolve<ConsoleLog>();
                }

                using (var scope2 = scope1.BeginLifetimeScope())
                {
                    for (int i = 0; i < 3; i++)
                    {
                        scope2.Resolve<ConsoleLog>();
                    }
                }
            }

            using (var scope3 = container.BeginLifetimeScope())
            {
                scope3.Resolve<ConsoleLog>();
            }


                //Console.WriteLine(container.Resolve<Child>().Parent);
                Console.ReadLine();
        }
    }
}

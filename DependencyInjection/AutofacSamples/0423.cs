﻿using Autofac;
using Autofac.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AutofacSamples423
{
    public interface ILog : IDisposable
    {
        void Write(string message);
    }
    //public interface IConsole { }

    public class ConsoleLog : ILog
    {
        public ConsoleLog()
        {
            Console.WriteLine($"Console log created at {DateTime.Now.Ticks}");
        }

        public void Write(string message)
        {
            Console.WriteLine(message);
        }
        public void Dispose()
        {
            Console.WriteLine($"Console log no longer required.");
        }

    }
    public class EmailLog : ILog
    {
        private const string adminEmail = "admin@foo.com";

        public void Dispose()
        {
        }

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

        public void Dispose()
        {
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

    public class Reporting
    {
        private Lazy<ConsoleLog> log;
        public Reporting(Lazy<ConsoleLog> log)
        {
            if (log == null)
            {
                throw new ArgumentNullException();
            }
            this.log = log;
            Console.WriteLine("Reporting component created");
        }
        public void Report()
        {
            log.Value.Write("Log started");
        }
    }
    class Program
    {
        static void Main0423(string[] args)
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<ConsoleLog>();
            builder.RegisterType<Reporting>();

            using (var c = builder.Build()) 
            {
                c.Resolve<Reporting>().Report();
            }

                Console.ReadLine();
        }
    }
}

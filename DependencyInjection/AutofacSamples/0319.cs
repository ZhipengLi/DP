﻿using Autofac;
using Autofac.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AutofacSamples0319
{
    public interface ILog
    {
        void Write(string message);
    }
    public interface IConsole { }

    public class ConsoleLog : ILog, IConsole
    {
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
    class Program
    {
        static void Main0319(string[] args)
        {
            var assembly = Assembly.GetExecutingAssembly();
            var builder = new ContainerBuilder();
            builder.RegisterAssemblyTypes(assembly)
                .Where(t => t.Name.EndsWith("Log"))
                .Except<SMSLog>()
                .Except<ConsoleLog>(c => c.As<ILog>().SingleInstance())
                .AsSelf();

            builder.RegisterAssemblyTypes()
                .Except<SMSLog>()
                .Where(t => t.Name.EndsWith("Log"))
                .As(t => t.GetInterfaces()[0]);

            Console.ReadLine();
        }
    }
}

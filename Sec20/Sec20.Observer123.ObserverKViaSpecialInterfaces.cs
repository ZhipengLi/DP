using Autofac;
using Autofac.Core.Activators;
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
using NUnit.Framework;

namespace Sec20.Observer123
{
    public class Event
    {

    }
    public class FallsIllEvent : Event
    {
        public string Address;

    }
    public class RecoveryEvent : Event
    {
        public string Address;

    }
    public class Person : IObservable<Event>
    {
        private readonly HashSet<Subscription> subscriptions
             = new HashSet<Subscription>();

        public IDisposable Subscribe(IObserver<Event> observer)
        {
            var subscription = new Subscription(this, observer);
            subscriptions.Add(subscription);
            return subscription;
        }
        public void FallIll()
        {
            foreach (var s in subscriptions)
            {
                s.Observer.OnNext(new FallsIllEvent { Address = "123 London Rd" });
            }
        }
        public void Recover()
        {
            foreach (var s in subscriptions)
            {
                s.Observer.OnNext(new RecoveryEvent { Address = "321 New York Rd" });
            }
        }
        private class Subscription : IDisposable
        {
            private readonly Person person;
            public readonly IObserver<Event> Observer;
            public Subscription(Person person, IObserver<Event> observer)
            {
                this.person = person;
                Observer = observer;
            }
            public void Dispose()
            {
                person.subscriptions.Remove(this);
            }
        }
    }
    public class Demo : IObserver<Event>
    {
        public List<string> Logs = new List<string>();
        public Person Person;
        //static void Main(string[] args)
        //{
        //    main();
        //    ReadLine();
        //}
        static void main()
        {
            var d = new Demo();
            d.TriggerFallIll();
            d.TriggerRecovery();
        }

        public Demo()
        {
            this.Person = new Person();
            IDisposable sub = Person.Subscribe(this);

            Person.OfType<FallsIllEvent>()
                .Subscribe(
                    args =>
                    {
                        Console.WriteLine($"A doctor is required at {args.Address}");
                        Logs.Add($"A doctor is required at {args.Address}");
                    }
                );
        }
        public void TriggerFallIll()
        {
            this.Person.FallIll();
        }
        public void TriggerRecovery()
        {
            this.Person.Recover();
        }

        public void OnCompleted() { }

        public void OnError(Exception error) { }

        public void OnNext(Event value)
        {
            if (value is RecoveryEvent args)
            {
                Console.WriteLine($"The patient recovers at {args.Address}");
                Logs.Add($"The patient recovers at {args.Address}");
            }
        }
    }
    //=============================================================================
    [TestFixture]
    public class Tests
    {
        [Test]
        public void ClassTest()
        {
            Person p = new Person();
            Assert.IsTrue(p is IObservable<Event>);
            Assert.AreEqual("HashSet`1", p.GetType().GetField("subscriptions",
                System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
                .FieldType.Name);
            Assert.IsTrue(p.GetType().GetNestedType("Subscription",
                System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance).GetInterfaces().Contains(typeof(IDisposable)));

            Demo d = new Demo();
            Assert.IsTrue(d is IObserver<Event>);
        }
        [Test]
        public void BasicTest()
        {
            var demo = new Demo();
            demo.TriggerFallIll();
            Assert.AreEqual("A doctor is required at 123 London Rd", demo.Logs.LastOrDefault());
            demo.TriggerRecovery();
            Assert.AreEqual("The patient recovers at 321 New York Rd", demo.Logs.LastOrDefault());
        }
    }
}
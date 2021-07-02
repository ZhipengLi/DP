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

namespace Sec20.Observer123
{
    public class Event
    { 
        
    }
    public class FallsIllEvent : Event
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
        //static void Main(string[] args)
        //{
        //    main();
        //    ReadLine();
        //}
        static void main()
        {
            var d = new Demo();
        }

        public Demo()
        {
            var person = new Person();
            //IDisposable sub = person.Subscribe(this);
            //person.FallIll();


            person.OfType<FallsIllEvent>()
                .Subscribe(
                    args => Console.WriteLine($"A doctor is required at {args.Address}")
                );

            person.FallIll();

        }

        public void OnCompleted() { }

        public void OnError(Exception error) { }

        public void OnNext(Event value)
        {
            if (value is FallsIllEvent args)
            {
                Console.WriteLine($"A doctor is requires at {args.Address}");
            }
        }
    }

}
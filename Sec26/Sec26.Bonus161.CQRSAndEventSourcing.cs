using Autofac.Core.Activators;
using NUnit.Framework;
using NUnit.Framework.Internal;
using NUnit.Framework.Internal.Execution;
using System;
using System.CodeDom;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Numerics;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using static System.Console;

namespace Sec26.Bonus161
{
    // CQRS = Command query responsibility segregation
    // CQS = Command query separation

    // Command = do/change
    public class Person
    {
        private int age;
        EventBroker broker;
        public Person(EventBroker broker)
        {
            this.broker = broker;
            broker.Commands += BrokerOnCommands;
            broker.Queries += BrokerOnQueries;
        }

        private void BrokerOnQueries(object sender, Query query)
        {
            var ac = query as AgeQuery;
            if (ac != null && ac.Target == this)
            {
                ac.Result = age;
            }
        }

        private void BrokerOnCommands(object sender, Command e)
        {
            var cac = e as ChangeAgeCommand;
            if (cac != null && cac.Target == this)
            {
                if(cac.Register)
                    broker.AllEvents.Add(new AgeChangedEvent(this, age, cac.Age));
                this.age = cac.Age;
            }
        }
    }
    public class EventBroker
    {
        // 1. All events that happened
        public IList<Event> AllEvents = new List<Event>();
        // 2. Commands
        public event EventHandler<Command> Commands;
        // 3. Query
        public event EventHandler<Query> Queries;

        public void Command(Command c)
        {
            Commands?.Invoke(this, c);
        }
        public T Query<T>(Query q)
        {
            Queries?.Invoke(this, q);
            return (T)q.Result;
        }
        public void UndoLast()
        {
            var e = AllEvents.LastOrDefault();
            var ac = e as AgeChangedEvent;
            if (ac != null)
            {
                Command(new ChangeAgeCommand(ac.Target, ac.OldValue) { Register = false });
                AllEvents.Remove(e);
            }
        }
    }

    public class Query
    {
        public object Result;
    }
    public class AgeQuery : Query
    {
        public Person Target;
        public AgeQuery(Person p)
        {
            this.Target = p;
        }

    }
    public class Event
    { 
        // backtrack
    }
    class AgeChangedEvent : Event
    {
        public Person Target;
        public int OldValue, NewValue;
        public AgeChangedEvent(Person target, int oldValue, int newValue)
        {
            this.Target = target;
            this.OldValue = oldValue;
            this.NewValue = newValue;
        }
        public override string ToString()
        {
            return $"Age changed from {OldValue} to {NewValue}.";
        }
    }
    public class Command : EventArgs
    {
        public bool Register = true;
    }

    class ChangeAgeCommand : Command
    {
        public Person Target;
        public int Age;
        public ChangeAgeCommand(Person target, int age)
        {
            Target = target;
            Age = age;

        }
    }
    //==============================================================================================


    public class Demo
    {
        static void Main(string[] args)
        {
            main();
            ReadLine();
        }
        static void main()
        {
            var eb = new EventBroker();
            var p = new Person(eb);
            eb.Command(new ChangeAgeCommand(p, 123));

            foreach (var e in eb.AllEvents)
            {
                WriteLine(e);
            }

            int age;
            age = eb.Query<int>(new AgeQuery(p) { Target = p });
            WriteLine(age);

            eb.UndoLast();
            age = eb.Query<int>(new AgeQuery(p) { Target = p });
            foreach (var e in eb.AllEvents)
            {
                WriteLine(e);
            }
        }
    }

    //==============================================================================================
    [TestFixture]
    public class Tests
    {
        [Test]
        public void BasicClasses()
        {
            var ageQuery = new AgeQuery(new Person(new EventBroker())) { Result = null };
            Assert.IsTrue(ageQuery is Query);

            var command = new Command() { Register = false };
            Assert.IsTrue(command is EventArgs);

            var changeCommand = new ChangeAgeCommand(new Person(new EventBroker()), 10);
            Assert.IsTrue(command is Command);


            var ageChangedEvent = new AgeChangedEvent(new Person(new EventBroker()), 10, 20);
            Assert.IsTrue(ageChangedEvent is Event);

            ageChangedEvent.OldValue = 10;
            ageChangedEvent.NewValue = 20;
            Assert.AreEqual("Age changed from 10 to 20.", ageChangedEvent.ToString());

            Type type = typeof(Person);
            Assert.IsNotNull(type.GetMethod("BrokerOnQueries", BindingFlags.NonPublic | BindingFlags.Instance));
            Assert.IsNotNull(type.GetMethod("BrokerOnCommands", BindingFlags.NonPublic | BindingFlags.Instance));
        }
        [Test]
        public void BasicTest()
        {
            var eb = new EventBroker();
            var p = new Person(eb);
            eb.Command(new ChangeAgeCommand(p, 123));

            Assert.IsTrue(eb.AllEvents.Count == 1);

            string output = eb.AllEvents[0].ToString();
            Assert.AreEqual(output, "Age changed from 0 to 123.");

            int age = eb.Query<int>(new AgeQuery(p));
            Assert.AreEqual(123, age);

            eb.UndoLast();
            Assert.IsTrue(eb.AllEvents.Count == 0);
        }
    }
}

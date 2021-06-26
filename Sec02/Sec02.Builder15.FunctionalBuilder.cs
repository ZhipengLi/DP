using NUnit.Framework;
using NUnit.Framework.Internal;
using NUnit.Framework.Internal.Execution;
using Sec02.Builder15;
using System;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace Sec02.Builder15
{
    public class Person
    {
        public string Name, Position;
        public override string ToString()
        {
            return $"{Name} is a {Position}.";
        }
    }
    public abstract class FunctionalBuilder<TSubject, TSelf>
        where TSelf : FunctionalBuilder<TSubject, TSelf>
        where TSubject : new()
    {
        private readonly List<Func<TSubject, TSubject>> actions
            = new List<Func<TSubject, TSubject>>();


        public TSelf Do(Action<TSubject> action)
            => AddAction(action);
        public TSubject Build()
            => actions.Aggregate(new TSubject(), (p, f) => f(p));
        private TSelf AddAction(Action<TSubject> action)
        {
            actions.Add(p => { action(p); return p; });
            return (TSelf)this;
        }
    }

    public sealed class PersonBuilder
        : FunctionalBuilder<Person, PersonBuilder>
    {
        public PersonBuilder Called(string name)
            => Do(p => p.Name = name);
    }
    //public sealed class PersonBuilder
    //{
    //    private readonly List<Func<Person, Person>> actions
    //        = new List<Func<Person, Person>>();

    //    public PersonBuilder Called(string name)
    //        => Do(p => p.Name = name);
    //    public PersonBuilder Do(Action<Person> action)
    //        => AddAction(action);
    //    public Person Build()
    //        => actions.Aggregate(new Person(), (p, f) => f(p));
    //    private PersonBuilder AddAction(Action<Person> action)
    //    {
    //        actions.Add(p => { action(p); return p; });
    //        return this;
    //    }
    //}

    public static class PersonBuilderExtensions
    {
        public static PersonBuilder WorksAs
            (this PersonBuilder builder, string position)
            => builder.Do(p => p.Position = position);
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
            var person = new PersonBuilder()
                .Called("Sarah")
                .WorksAs("Developer")
                .Build();
            WriteLine(person);
        }
    }

    //==============================================================================================
    [TestFixture]
    public class Tests
    {

        [Test]
        public void BasicTest()
        {
            var builder = new PersonBuilder()
                .Called("Sarah")
                .WorksAs("Developer");

            Assert.IsTrue(builder is PersonBuilder);
            Assert.IsTrue(builder is FunctionalBuilder<Person, PersonBuilder>);

            Type type = typeof(PersonBuilder);
            Assert.IsNotNull(type.GetMethod("Called"));
            
            // Check extension methods:
            //
            Assert.IsNull(type.GetMethod("WorksAs"));

            Assert.IsNotNull(type.GetMethod("Do"));
            Assert.IsNotNull(type.GetMethod("Build"));
            Assert.IsNull(type.GetMethod("AddAction", BindingFlags.NonPublic | BindingFlags.Instance));

            type = typeof(FunctionalBuilder<Person, PersonBuilder>);
            Assert.IsNotNull(type.GetMethod("AddAction", BindingFlags.NonPublic | BindingFlags.Instance));

            var p = builder.Build();
            Assert.AreEqual(p.Name, "Sarah");
            Assert.AreEqual(p.Position, "Developer");

        }
    }
}

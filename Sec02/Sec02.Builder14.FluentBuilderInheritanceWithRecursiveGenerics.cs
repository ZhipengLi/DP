using NUnit.Framework;
using NUnit.Framework.Internal;
using NUnit.Framework.Internal.Execution;
using System;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using static System.Console;

namespace Sec02.Builder14
{
    public class Person
    {

        public string Name;
        public string Position;
        public class Builder : PersonJobBuilder<Builder>
        {

        }
        public static Builder New => new Builder();
        public override string ToString()
        {
            return $"{nameof(Name)}: {Name}, {nameof(Position)}: {Position}";
        }
    }
    public abstract class PersonBuilder
    {
        protected Person person = new Person();
        public Person Build()
        {
            return person;
        }
    }

    // class Foo : Bar<Foo>
    public class PersonInfoBuilder<SELF>
        : PersonBuilder
        where SELF : PersonInfoBuilder<SELF>
    {
        public SELF Called(string name)
        {
            person.Name = name;
            return (SELF)this;
        }
    }
    public class PersonJobBuilder<SELF> : PersonInfoBuilder<PersonJobBuilder<SELF>>
         where SELF: PersonJobBuilder<SELF>
    {
        public SELF WorksAsA(string position)
        {
            person.Position = position;
            return (SELF)this;
        }
    }
    //==============================================================================================

    public class Demo
    {
        //static void Main(string[] args)
        //{
        //    main();
        //    ReadLine();
        //}
        static void main()
        {
            var me = Person.New
                .Called("Dmitri")
                 .WorksAsA("quant")
                 .Build();
            WriteLine(me);
        }
    }

    //==============================================================================================
    [TestFixture]
    public class Tests
    {

        [Test]
        public void BasicTest()
        {
            var builder = Person.New
                        .Called("Dmitri")
                         .WorksAsA("quant");
            Assert.IsTrue(builder is Person.Builder);
            Assert.IsTrue(builder is PersonBuilder);
            Assert.IsTrue(builder is PersonJobBuilder<Person.Builder>);
            Assert.IsTrue(builder is PersonInfoBuilder<PersonJobBuilder<Person.Builder>>);

            var p = Person.New
            .Called("Dmitri")
             .WorksAsA("quant")
             .Build();
            Assert.AreEqual(p.Name, "Dmitri");
            Assert.AreEqual(p.Position, "quant");
        }
    }
}

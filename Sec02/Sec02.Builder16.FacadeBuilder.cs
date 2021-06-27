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

namespace Sec02.Builder16
{
    public class Person
    {
        public string StreetAddress, Postcode, City;

        public string CompanyName, Position;
        public int AnnualIncome;

        public override string ToString()
        {
            return $"{StreetAddress}, {Postcode}, {City}, {CompanyName}, {Position}, {AnnualIncome}.";
        }
    }

    // facade
    public class PersonBuilder
    {
        // reference
        protected Person person = new Person();

        public PersonJobBuilder Works => new PersonJobBuilder(person);
        public PersonAddressBuilder Lives => new PersonAddressBuilder(person);
        public static implicit operator Person(PersonBuilder pb)
        {
            return pb.person;
        }
    }
    public class PersonJobBuilder : PersonBuilder
    {
        public PersonJobBuilder(Person person)
        {
            this.person = person;
        }

        public PersonJobBuilder At(string compnayName)
        {
            this.person.CompanyName = compnayName;
            return this;
        }
        public PersonJobBuilder AsA(string position)
        {
            person.Position = position;
            return this;
        }
        public PersonJobBuilder Earning(int amount)
        {
            person.AnnualIncome = amount;
            return this;
        }
    }
    public class PersonAddressBuilder : PersonBuilder
    {
        public PersonAddressBuilder(Person person)
        {
            this.person = person;
        }
        public PersonAddressBuilder At(string street)
        {
            this.person.StreetAddress = street;
            return this;
        }
        public PersonAddressBuilder In(string city)
        {
            this.person.City = city;
            return this;
        }
        public PersonAddressBuilder WithPostCode(string postcode)
        {
            this.person.Postcode = postcode;
            return this;
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
            var pb = new PersonBuilder();
            Person person = pb
                .Lives.At("123 street")
                .In("London")
                .WithPostCode("12345")
                .Works.At("Fabrikam")
                .AsA("Engineer")
                .Earning(123000);
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


        }
    }
}

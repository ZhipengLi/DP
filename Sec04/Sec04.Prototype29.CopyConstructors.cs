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
using System.Data.Common;
using System.Linq;
using System.Numerics;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using static System.Console;

namespace Sec04.Prototype29
{
    public class Person
    {
        public string[] Names;
        public Address Address;
        public Person(string[] names, Address address)
        {
            this.Names = names;
            this.Address = address;
        }

        public object Clone()
        {
            return new Person(Names, (Address)Address.Clone());
        }

        public override string ToString()
        {
            return $"{string.Join(" ", Names)}, {Address}.";
        }
        public Person(Person other)
        {
            Names = other.Names;
            Address = new Address(other.Address);
        }
    }

    public class Address
    {
        public string StreetName;
        public int HouseNumber;
        public Address(string streeName, int houseNumber)
        {
            this.StreetName = streeName;
            this.HouseNumber = houseNumber;
        }
        public Address(Address other)
        {
            this.StreetName = other.StreetName;
            this.HouseNumber = other.HouseNumber;
        }

        public object Clone()
        {
            return new Address(this.StreetName, this.HouseNumber);
        }

        public override string ToString()
        {
            return $"{StreetName}, {HouseNumber}";
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
            var john = new Person(new[] { "John", "Smith" },
                new Address("London Road", 123));

            var jane = new Person(john);
            jane.Address.HouseNumber = 321;

            WriteLine(john);
            WriteLine(jane);
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

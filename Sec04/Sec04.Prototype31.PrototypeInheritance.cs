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

namespace Sec04.Prototype31
{
    public interface IDeepCopyable<T>
        where T : new()
    {
        //void CopyTo(T target);
        T DeepCopy();
    }
    public class Address : IDeepCopyable<Address>
    {
        public string StreetName;
        public int HouseNumber;
        public Address() { }
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

        public override string ToString()
        {
            return $"{StreetName}, {HouseNumber}";
        }

        public Address DeepCopy()
        {
            return (Address)MemberwiseClone();
        }
    }
    public class Person : IDeepCopyable<Person>
    {
        public string[] Names;
        public Address Address;
        public Person() { }
        public Person(string[] names, Address address)
        {
            this.Names = names;
            this.Address = address;
        }

        public Person DeepCopy()
        {
            return new Person((string[])Names.Clone(), Address.DeepCopy());
        }

        public override string ToString()
        {
            return $"{string.Join(" ", Names)}, {Address}.";
        }
    }
    public class Employee : Person, IDeepCopyable<Employee>
    {
        public int Salary;
        public Employee():base()
        { }
        public Employee(string[] names, Address address,
            int salary) : base(names, address)
        {
            this.Salary = salary;
        }
        public override string ToString()
        {
            return $"{base.ToString()}, {nameof(Salary)}: {Salary}";
        }

        //public Employee IDeepCopyable<Employee>.DeepCopy()
        //{
        //    return new Employee((string[])Names.Clone(), Address.DeepCopy(), Salary);
        //}

        public Employee DeepCopy()
        {
            return new Employee((string[])Names.Clone(), Address.DeepCopy(), Salary);
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
            var john = new Employee();
            john.Names = new[] { "John", "Doe" };
            john.Address = new Address()
            {
                HouseNumber = 123,
                StreetName = "London Road"
            };
            john.Salary = 321000;
            var copy = john.DeepCopy();

            copy.Names[1] = "Smith";
            copy.Address.HouseNumber++;
            copy.Salary = 123000;
            WriteLine(john);
            WriteLine(copy);
            new[] { 1, 2, 3 }.Clone();
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

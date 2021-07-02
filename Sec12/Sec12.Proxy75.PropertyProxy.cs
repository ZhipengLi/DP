using Autofac;
using Autofac.Features.Metadata;
//using JetBrains.dotMemoryUnit;
using MoreLinq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace Sec12.Proxy75
{
    public class Property<T> where T : new()
    {
        private T value;
        public T Value
        {
            get => value;
            set 
            {
                if (Equals(this.value, value)) return;
                WriteLine($"Assigning value to {value}");
                this.value = value;
            }

        }
        public Property() : this(Activator.CreateInstance<T>())
        {
        
        }
        public Property(T value)
        {
            WriteLine("new property created");
            this.value = value;
        }
        public static implicit operator T(Property<T> property)
        {
            return property.value; // int n = p_int
        }
        public static implicit operator Property<T>(T value)
        {
            return new Property<T>(value); // Property<int> p = 123;
        }
        public override int GetHashCode()
        {
            return value.GetHashCode();
        }
    }
    public class Creature
    {
        //public Property<int> Agility { get; set; }
        private Property<int> agility = new Property<int>();
        public int Agility
        {
            get => agility.Value;
            set => agility.Value = value;
        }
    }
    class Demo
    {
        //static void Main(string[] args)
        //{
        //    main();
        //    ReadLine();
        //}
        static void main()
        {
            var c = new Creature();
            c.Agility = 10;
            c.Agility = 10;
        }
    }
}

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

namespace Sec03.Builder25
{

    public interface IHotDrink
    {
        void Consume();
    }

    internal class Tea : IHotDrink
    {
        public void Consume()
        {
            WriteLine("This tea is nice but I'd prefer it with milk.");
        }
    }

    internal class Coffee : IHotDrink
    {
        public void Consume()
        {
            WriteLine("This coffee is sensational!");
        }
    }

    public interface IHotDrinkFactory
    {
        IHotDrink Prepare(int amount);
    }

    internal class TeaFactory : IHotDrinkFactory
    {
        public IHotDrink Prepare(int amount)
        {
            WriteLine($"Put in a tea bag, boil water, pour {amount} ml, add lemon, enjoy!");
            return new Tea();
        }

    }
    internal class CoffeeFactory : IHotDrinkFactory
    {
        public IHotDrink Prepare(int amount)
        {
            WriteLine($"Grind beans, boil water, pour {amount} ml, add cream & sugar, ejoy!");
            return new Coffee();
        }
    }

    public class HotDrinkMachine
    {
        //public enum AvailableDrink
        //{
        //    Coffee, Tea
        //}
        //private Dictionary<AvailableDrink, IHotDrinkFactory> factories =
        //    new Dictionary<AvailableDrink, IHotDrinkFactory>();

        //public HotDrinkMachine()
        //{
        //    foreach (AvailableDrink drink in Enum.GetValues(typeof(AvailableDrink)))
        //    {
        //        var factory = (IHotDrinkFactory)Activator.CreateInstance(
        //                Type.GetType("Sec03.Builder24." + Enum.GetName(typeof(AvailableDrink), drink) + "Factory")
        //            );
        //        factories.Add(drink, factory);
        //    }
        //}
        //public IHotDrink MakeDrink(AvailableDrink drink, int amount)
        //{
        //    return factories[drink].Prepare(amount);
        //}
        private List<Tuple<string, IHotDrinkFactory>> factories =
             new List<Tuple<string, IHotDrinkFactory>>();
        public HotDrinkMachine()
        {
            foreach(var t in typeof(HotDrinkMachine).Assembly.GetTypes())
            {
                if (typeof(IHotDrinkFactory).IsAssignableFrom(t) &&
                        !t.IsInterface)
                {
                    factories.Add(Tuple.Create(
                        t.Name.Replace("Factory", string.Empty),
                        (IHotDrinkFactory)Activator.CreateInstance(t)
                        ));
                }
            }
        }
        public IHotDrink MakeDrink()
        {
            WriteLine("Available drinks:");
            for (var index = 0; index < factories.Count; index++)
            {
                var tuple = factories[index];
                WriteLine($"{index}: {tuple.Item1}");
            }
            while (true)
            {
                string s;
                if ((s = Console.ReadLine()) != null
                    && int.TryParse(s, out int i)
                    && i >=0
                    && i<factories.Count
                    )
                {
                    WriteLine("Specify amount: ");
                    s = ReadLine();
                    if (s != null
                        && int.TryParse(s, out int amount)
                        && amount > 0)
                    {
                        return factories[i].Item2.Prepare(amount);
                    }
                }
                WriteLine("Incorrect input. Try again!");
            }
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
            var machine = new HotDrinkMachine();
            machine.MakeDrink();
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

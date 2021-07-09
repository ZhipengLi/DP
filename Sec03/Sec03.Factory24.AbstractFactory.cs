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
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using static System.Console;

namespace Sec03.Factory24
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
        public enum AvailableDrink
        { 
            Coffee, Tea
        }
        private Dictionary<AvailableDrink, IHotDrinkFactory> factories =
            new Dictionary<AvailableDrink, IHotDrinkFactory>();

        public HotDrinkMachine()
        {
            foreach (AvailableDrink drink in Enum.GetValues(typeof(AvailableDrink)))
            {
                var factory = (IHotDrinkFactory)Activator.CreateInstance(
                        Type.GetType("Sec03.Factory24." + Enum.GetName(typeof(AvailableDrink), drink)+"Factory")
                    );
                factories.Add(drink, factory);
            }
        }
        public IHotDrink MakeDrink(AvailableDrink drink, int amount)
        {
            return factories[drink].Prepare(amount);
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
            var machine = new HotDrinkMachine();
            var drink = machine.MakeDrink(HotDrinkMachine.AvailableDrink.Tea, 100);
            drink.Consume();
        }
    }

    //==============================================================================================
    [TestFixture]
    public class Tests
    {
        [Test]
        public void ClassTest()
        {
            Assert.IsTrue(typeof(IHotDrink).IsInterface);
            Assert.IsTrue(typeof(IHotDrinkFactory).IsInterface);
            Assert.IsNotNull(typeof(IHotDrink).GetMethod("Consume"));
            Assert.IsNotNull(typeof(IHotDrinkFactory).GetMethod("Prepare"));
        }
        [Test]
        public void BasicTest()
        {
            var machine = new HotDrinkMachine();
            var drink1 = machine.MakeDrink(HotDrinkMachine.AvailableDrink.Tea, 100);
            var drink2 = machine.MakeDrink(HotDrinkMachine.AvailableDrink.Coffee, 100);

            Assert.IsTrue(drink1 is Tea);
            Assert.IsTrue(drink2 is Coffee);

            Type factoryType = typeof(TeaFactory);
            Assert.IsNotNull(factoryType.GetMethod("Prepare"));

            Type drinkType = typeof(Tea);
            Assert.IsNotNull(drinkType.GetMethod("Consume"));
        }
    }
}

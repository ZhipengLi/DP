using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using static System.Console;
using System.Runtime.Serialization.Formatters.Binary;
using MoreLinq;
using System.ComponentModel;
using NUnit.Framework;

namespace Sec05.Singleton35
{
    public interface IDatabase
    {
        int GetPopulation(string name);
    }
    public class SingletonDatabase : IDatabase
    {
        private Dictionary<string, int> capitals;
        private SingletonDatabase()
        {
            WriteLine("Initialinzing Database");
            capitals = File.ReadAllLines("capitals.txt")
                .Batch(2)
                .ToDictionary(
                    list => list.ElementAt(0).Trim(),
                    list => int.Parse(list.ElementAt(1))
                );
        }
        public int GetPopulation(string name)
        {
            return capitals[name];
        }

        private static Lazy<SingletonDatabase> instance = new Lazy<SingletonDatabase>( () => new SingletonDatabase());

        public static SingletonDatabase Instance => instance.Value;
    }

    public class Demo
    {
        //static void Main(string[] args)
        //{
        //    main();
        //    ReadLine();
        //}
        static void main()
        {
            var db = SingletonDatabase.Instance;
            var city = "Tokyo";
            WriteLine($"{city} has population { db.GetPopulation(city)}");
        }
    }

    //==============================================================================================
    [TestFixture]
    public class Tests
    {

        [Test]
        public void BasicTest()
        {
            //var db = SingletonDatabase.Instance;
            //var city = "Tokyo";
            //Assert.AreEqual(33200000, db.GetPopulation(city));
        }
    }
}

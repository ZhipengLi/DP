using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace Sec16.Iterator103
{
    public class Creature : IEnumerable<int>
    {
        private int[] stats = new int[3];
        private const int strength = 0;
        public int Strength 
        {
            get => stats[strength];
            set => stats[strength] = value;
        }
        public int Agility { get; set; }
        public int Intelligence { get; set; }
        public double AverageStat
        {
            get
            {
                return (Strength + Agility + Intelligence) / 3.0;
            }
        }

        public IEnumerator<int> GetEnumerator()
        {
            return stats.AsEnumerable().GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public int this[int index]
        { 
            get { return stats[index]; }
            set { stats[index] = value; }
        }
    }
    class Demo
    {
        static void Main(string[] args)
        {
            main();
            ReadLine();
        }
        static void main()
        {
            var root = new Node<int>(1, new Node<int>(2), new Node<int>(3));
            var it = new InOrderIterator<int>(root);
            while (it.ModeNext())
            {
                WriteLine(it.Current.Value);
                WriteLine(',');
            }
            WriteLine();
        }
    }

}


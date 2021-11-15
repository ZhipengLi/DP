using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static System.Console;

namespace Sec06.ParallelLINQ38
{
    class Program
    {
        //static void Main(string[] args)
        //{
        //    main();
        //    ReadLine();
        //}
        static void main()
        {
            const int count = 50;
            var items = Enumerable.Range(1, count).ToArray();
            var results = new int[count];
            items.AsParallel().ForAll(x => 
            {
                int newValue = x * x * x;
                Console.Write($"{newValue} ({Task.CurrentId})\t");
                results[x - 1] = newValue;
            });

            Console.WriteLine();
            Console.WriteLine();

            var cubes = Enumerable.Range(1, count).AsParallel().AsOrdered().Select(x => x * x * x);
            foreach (var i in cubes)
            {
                Console.Write($"{i}\t");
            }
            WriteLine("Main program done.");
        }
    }
}



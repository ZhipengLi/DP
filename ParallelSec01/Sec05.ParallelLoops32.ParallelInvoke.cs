using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static System.Console;

namespace Sec05.ParallelLoops32
{
    class Program
    {
        public static IEnumerable<int> Range(int start, int end, int step)
        {
            for (int i = start; i < end; i += step)
            {
                yield return i;
            }
        }
        //static void Main(string[] args)
        //{
        //    main();
        //    ReadLine();
        //}
        static void main()
        {
            var a = new Action(() => Console.WriteLine($"First {Task.CurrentId}"));
            var b = new Action(() => Console.WriteLine($"Second {Task.CurrentId}"));
            var c = new Action(() => Console.WriteLine($"Third {Task.CurrentId}"));

            Parallel.Invoke(a, b, c);

            Parallel.For(1, 11, i => 
            {
                Console.WriteLine($"{i * i} \t");
            });
            //string[] words = { "oh", "what", "a", "night" };
            //Parallel.ForEach(words, word => 
            //{
            //    Console.WriteLine($"{word} has length {word.Length} (task {Task.CurrentId})");
            //});
            //WriteLine("Main program done.");
            Console.WriteLine();
            Parallel.ForEach(Range(1, 20, 3), i => Console.WriteLine(i));

        }
    }
}



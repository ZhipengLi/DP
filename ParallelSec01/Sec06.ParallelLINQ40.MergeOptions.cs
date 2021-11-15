using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static System.Console;

namespace Sec06.ParallelLINQ40
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
            var numbers = Enumerable.Range(1, 20).ToArray();

            var results = numbers.AsParallel()
                .WithMergeOptions(ParallelMergeOptions.FullyBuffered)
                .Select(x => 
            {
                var result = Math.Log10(x);
                Console.Write($"Produced {result} \t");
                return result;
            });

            foreach (var result in results)
            {
                Console.WriteLine($"Consumed {result} \t");
            }
            WriteLine("Main program done.");
        }
    }
}



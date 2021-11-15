using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static System.Console;

namespace Sec06.ParallelLINQ41
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
            //var sum = Enumerable.Range(1, 1000).Sum();

            //var sum = Enumerable.Range(1, 1000).Aggregate(0, (i, acc) => i + acc);

            var sum = ParallelEnumerable.Range(1, 2)
                .Aggregate(
                    1,
                    (partialSum, i) => partialSum += i,
                    (total, subtotal) => total += subtotal,
                    i => i+10);
            Console.WriteLine($"1...1000 is {sum}");
            WriteLine("Main program done.");
        }
    }
}



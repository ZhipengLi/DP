using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static System.Console;

namespace Sec05.ParallelLoops34
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
            //int sum = 0;
            //Parallel.For(1, 1001, x => 
            //{
            //    Interlocked.Add(ref sum, x);
            //});
            int sum = 0;
            Parallel.For(1, 1001, () => 0, 
                (x, state, tls) => 
                {
                    tls += x;
                    Console.WriteLine($"Task {Task.CurrentId} has sum {tls}");
                    return tls;
                }, 
                partialSum => 
                {
                    Console.WriteLine($"Partial value of task {Task.CurrentId} is {partialSum}");
                    Interlocked.Add(ref sum, partialSum);
                });
            Console.WriteLine($"Sum of 1..100 = {sum}");
            WriteLine("Main program done.");
        }
    }
}



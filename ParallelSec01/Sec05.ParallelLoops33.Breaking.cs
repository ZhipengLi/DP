using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static System.Console;

namespace Sec05.ParallelLoops33
{
    class Program
    {
        private static ParallelLoopResult result;
        //static void Main(string[] args)
        //{
        //    try
        //    {
        //        main();
        //    }
        //    catch (AggregateException ae)
        //    {
        //        ae.Handle(e =>
        //        {
        //            Console.WriteLine(e.Message);
        //            return true;
        //        });
        //    }
        //    catch (OperationCanceledException oce)
        //    { 
                
        //    }

        //    ReadLine();
        //}
        static void main()
        {
            var cts = new CancellationTokenSource();

            ParallelOptions po = new ParallelOptions();
            po.CancellationToken = cts.Token;

            result = Parallel.For(0, 20, po, (int x, ParallelLoopState state) => 
            {
                Console.WriteLine($"{x}[{Task.CurrentId}]\t");
                if (x == 10)
                {
                    //state.Stop();
                    //state.Break();
                    //throw new Exception();
                    cts.Cancel();
                }
            });
            Console.WriteLine();
            Console.WriteLine($"Was loop completed? {result.IsCompleted}");
            if (result.LowestBreakIteration.HasValue)
            {
                Console.WriteLine($"Lowest break iteration is {result.LowestBreakIteration}");
            }
            WriteLine("Main program done.");
        }
    }
}



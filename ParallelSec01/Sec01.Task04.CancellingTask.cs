using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static System.Console;

namespace Sec01.Task04
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
            var planned = new CancellationTokenSource();
            var preventative = new CancellationTokenSource();
            var emergency = new CancellationTokenSource();

            var paranoid = CancellationTokenSource.CreateLinkedTokenSource(
                planned.Token, preventative.Token, emergency.Token);

            Task.Factory.StartNew(()=> 
            {
                int i = 0;
                while (true)
                {
                    paranoid.Token.ThrowIfCancellationRequested();
                    Console.WriteLine($"{i++}\t");
                    Thread.Sleep(1000);
                }
            }, paranoid.Token);

            Console.ReadKey();
            emergency.Cancel();

            WriteLine("Main program done.");

        }
        static void main1()
        {
            var cts = new CancellationTokenSource();
            var token = cts.Token;

            token.Register(() => 
            {
                Console.WriteLine("Cancellation has been requested.");
            });
            var t = new Task(() => {
                int i = 0;
                while (true)
                {
                    token.ThrowIfCancellationRequested();

                    Console.WriteLine($"{i++}\t");
                }
            }, token);
            t.Start();

            Task.Factory.StartNew(() => 
            {
                token.WaitHandle.WaitOne();
                Console.WriteLine("Wait handle released, cancellation was requested.");
            });
            Console.ReadKey();
            cts.Cancel();

            WriteLine("Main program done.");
        }
    }
}

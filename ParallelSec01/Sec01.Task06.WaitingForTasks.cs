using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static System.Console;

namespace Sec01.Task06
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
            var cts = new CancellationTokenSource();
            var token = cts.Token;
            var t = new Task(() => 
            {
                Console.WriteLine("I take 5 seconds");
                for (int i = 0; i < 5; i++)
                {
                    token.ThrowIfCancellationRequested();
                    Thread.Sleep(1000);
                }

                Console.WriteLine("I'm done.");
            }, token);
            t.Start();
            Task t2 = Task.Factory.StartNew(() => 
            {
                Thread.Sleep(3000);
            }, token);
            //Task.WaitAll(t, t2);
            //Task.WaitAny(t, t2);

            Task.WaitAll(new[] { t, t2 }, 4000, token);
            Console.WriteLine($"Task t status is {t.Status}");
            Console.WriteLine($"Task t2 status is {t2.Status}");

            WriteLine("Main program done.");

        }

    }
}

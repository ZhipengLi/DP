using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static System.Console;

namespace Sec01.Task05
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
                Console.WriteLine("Press any key to disarm; you have 5 seconds");
                bool cancelled = token.WaitHandle.WaitOne(5000);
                Console.WriteLine(cancelled ? "Bomb disarmed." : "Boom!!!");
            }, token);
            t.Start();
            Console.ReadKey();
            cts.Cancel();

            WriteLine("Main program done.");

        }

    }
}

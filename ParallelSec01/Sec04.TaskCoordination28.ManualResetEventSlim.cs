using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static System.Console;

namespace Sec04.TaskCoordination28
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
            var evt = new ManualResetEventSlim();
            Task.Factory.StartNew(() => 
            {
                Console.WriteLine("Boiling water");
                evt.Set();
            });

            var makeTea = Task.Factory.StartNew(() => 
            {
                Console.WriteLine("Waiting for water ...");
                evt.Wait();
                Console.WriteLine("Here is your tea");
            });
            WriteLine("Main program done.");
        }
    }
}



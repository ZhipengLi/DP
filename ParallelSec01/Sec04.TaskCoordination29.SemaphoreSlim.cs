using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static System.Console;

namespace Sec04.TaskCoordination29
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
            var semaphore = new SemaphoreSlim(2, 10);
            for (int i = 0; i < 20; i++)
            {
                Task.Factory.StartNew(() => 
                {
                    Console.WriteLine($"Entering task {Task.CurrentId}");
                    semaphore.Wait();//ReleaseCount--
                    Console.WriteLine($"Processing task {Task.CurrentId}");
                });
            }
            while (semaphore.CurrentCount <= 2)
            {
                Console.WriteLine($"Semaphore count : {semaphore.CurrentCount}");
                Console.ReadKey();
                semaphore.Release(2);
            }
            WriteLine("Main program done.");
        }
    }
}



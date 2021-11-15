using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static System.Console;

namespace Sec04.TaskCoordination24
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
            //var task = Task.Factory.StartNew(() => 
            //{
            //    Console.WriteLine("Boiling water");
            //});
            //var task2 = task.ContinueWith(t => 
            //{
            //    Console.WriteLine($"Completed task {t.Id}, pour water into cup.");
            //});
            //task2.Wait();

            var task = Task.Factory.StartNew(() => "Task 1");
            var task2 = Task.Factory.StartNew(() => "Task 2");
            var task3 = Task.Factory.ContinueWhenAll(new[] { task, task2 },
                tasks =>
                {
                    Console.WriteLine("Tasks completed:");
                    foreach (var t in tasks)
                        Console.WriteLine(" - " + t.Result);
                    Console.WriteLine("All tasks done.");
                });
            WriteLine("Main program done.");
        }
    }
}



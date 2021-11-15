using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static System.Console;

namespace Sec04.TaskCoordination25
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
            var parent = new Task(() => 
            {
                var child = new Task(() => 
                {
                    Console.WriteLine("Child task starting.");
                    Thread.Sleep(3000);
                    Console.WriteLine("Child task finishing.");
                    //throw new Exception();
                }, TaskCreationOptions.AttachedToParent);
                var completionHandler = child.ContinueWith(t =>
                {
                    Console.WriteLine($"Hooray, task {t.Id}'s state is {t.Status}");
                }, TaskContinuationOptions.AttachedToParent | TaskContinuationOptions.OnlyOnRanToCompletion);
                var failHandler = child.ContinueWith(t =>
                {
                    Console.WriteLine($"Oops, task {t.Id}'s state is {t.Status}");
                }, TaskContinuationOptions.AttachedToParent | TaskContinuationOptions.OnlyOnFaulted);
                child.Start();
            });
            parent.Start();
            try
            {
                parent.Wait();
            }
            catch (AggregateException ae)
            {
                ae.Handle(e => true);
            }
            WriteLine("Main program done.");
        }
    }
}



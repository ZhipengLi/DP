using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static System.Console;

namespace Sec01.Task07
{
    class Program
    {
        //static void Main(string[] args)
        //{
        //    try
        //    {
        //        main();
        //    }
        //    catch (AggregateException ae)
        //    {
        //        foreach (var e in ae.InnerExceptions)
        //            Console.WriteLine($"Handled elsewhere: {e.GetType()} from {e.Source}.");
        //    }
        //    ReadLine();
        //}
        static void main()
        {
            var t = Task.Factory.StartNew(()=> 
            {
                throw new InvalidOperationException("Can't perform this!") { Source = "t" };
            });
            var t2 = Task.Factory.StartNew(() =>
            {
                throw new AccessViolationException("Can't access!") { Source = "t2" };
            });

            try
            {
                Task.WaitAll(t, t2);
            }
            catch (AggregateException ae)
            {
                ae.Handle(e => 
                {
                    if (e is InvalidOperationException)
                    {
                        Console.WriteLine("Invalid op!");
                        return true;
                    }
                    return false;
                });
            }
            WriteLine("Main program done.");
        }

    }
}

using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static System.Console;

namespace Sec07.AsynchronousProgramming48
{
    public class Foo
    {
        private Foo()
        { 
        
        }

        private async Task<Foo> InitAsync()
        {
            await Task.Delay(1000);
            return this;
        }

        public static Task<Foo> CreateAsync()
        {
            var result = new Foo();
            return result.InitAsync();
        }
    }
    class Program
    {
        //static async Task Main(string[] args)
        //{
        //    await main();
        //    ReadLine();
        //}
        static async Task main()
        {
            //var foo = new Foo();
            //await foo.InitAsync();

            Foo x = await Foo.CreateAsync();


            WriteLine("Main program done.");
        }
    }
}



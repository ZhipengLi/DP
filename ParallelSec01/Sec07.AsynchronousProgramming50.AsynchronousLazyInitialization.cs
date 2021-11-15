using Nito.AsyncEx;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static System.Console;

namespace Sec07.AsynchronousProgramming50
{
    public class Stuff
    {
        private static int value;
        private readonly Lazy<Task<int>> AutoIncValue =
            new Lazy<Task<int>>(async () =>
            {
                await Task.Delay(1000).ConfigureAwait(false);
                return value++;
            });
        private readonly Lazy<Task<int>> AutoIncValue2 =
        new Lazy<Task<int>>(() =>Task.Run(async() =>
        {
            await Task.Delay(1000).ConfigureAwait(false);
            return value++;
        }));

        // Nito.Asyncex
        private AsyncLazy<int> AutoIncValue3
            = new AsyncLazy<int>(async () =>
            {
                await Task.Delay(1000);
                return value++;
            });
        public async void UseValue()
        {
            int value = await AutoIncValue.Value;
        }
    }
    class Program
    {
        //static void Main(string[] args)
        //{
        //    main();
        //    ReadLine();
        //}
        static void main()
        {
            WriteLine("Main program done.");
        }
    }
}



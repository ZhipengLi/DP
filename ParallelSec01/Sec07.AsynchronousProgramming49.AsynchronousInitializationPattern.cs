using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static System.Console;

namespace Sec07.AsynchronousProgramming49
{
    public interface IAsyncInit
    { 
        Task InitTask { get; }
    }

    public class MyClass : IAsyncInit
    {
        public MyClass()
        {
            InitTask = InitAsync();
        }
        public Task InitTask { get; }
        private async Task InitAsync()
        {
            await Task.Delay(1000);
        }
    }
    public class MyOtherClass : IAsyncInit
    {
        private readonly MyClass myClass;
        public MyOtherClass(MyClass myClass)
        {
            this.myClass = myClass;
            InitTask = InitAsync();
        }
        public Task InitTask { get; }
        private async Task InitAsync()
        {
            if (myClass is IAsyncInit ai)
                await ai.InitTask;

            await Task.Delay(1000);
        }
    }
    class Program
    {
        //async static void Main(string[] args)
        //{
        //    await main();
        //    ReadLine();
        //}
        async static Task main()
        {
            var myClass = new MyClass();
            //if (myClass is IAsyncInit ai)
            //    await ai.InitTask;

            var oc = new MyOtherClass(myClass);
            await oc.InitTask;
        }
    }
}



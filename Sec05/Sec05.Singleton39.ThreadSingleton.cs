using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using static System.Console;
using System.Runtime.Serialization.Formatters.Binary;
using MoreLinq;
using System.ComponentModel;
using NUnit.Framework;
using Autofac;
using System.Threading;

namespace Sec05.Singleton39
{
    public sealed class PerThreadSingleton
    {
        private static ThreadLocal<PerThreadSingleton> threadInstance
             = new ThreadLocal<PerThreadSingleton>(
                 () => new PerThreadSingleton()
                 );
        public int Id;
        private PerThreadSingleton()
        {
            Id = Thread.CurrentThread.ManagedThreadId;
        }
        public static PerThreadSingleton Instance => threadInstance.Value;
    }
    class Demo
    {
        //static void Main(string[] args)
        //{
        //    var t1 = Task.Factory.StartNew(() =>
        //    {
        //        WriteLine("t1: " + PerThreadSingleton.Instance.Id);
        //    });
        //    var t2 = Task.Factory.StartNew(() =>
        //    {
        //        WriteLine("t2: " + PerThreadSingleton.Instance.Id);
        //        WriteLine("t2: " + PerThreadSingleton.Instance.Id);
        //    });
        //    Task.WaitAll(t1, t2);
        //    ReadLine();
        //}
    }
}

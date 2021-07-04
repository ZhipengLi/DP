using Autofac;
using Autofac.Features.Metadata;
using MoreLinq;
using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace Sec09.Decorator58
{
    public class MyStringBuilder
    {
        StringBuilder sb = new StringBuilder();

        public static implicit operator MyStringBuilder(string s)
        {
            var msb = new MyStringBuilder();
            msb.sb.Append(s);
            return msb;
        }

        public static MyStringBuilder operator +(MyStringBuilder msb, string s)
        {
            msb.Append(s);
            return msb;
        }
        public override string ToString()
        {
            return sb.ToString();
        }
        public MyStringBuilder Append(string str)
        {
            this.sb.Append(str);
            return this;
        }
    }
    //==============================================================================================
    //public class Demo
    //{
    //    static void Main(string[] args)
    //    {
    //        MyStringBuilder s = "hello";
    //        s += " world";
    //        WriteLine(s);

    //        ReadLine();
    //    }
    //}

    //============================================================================================
    [TestFixture]
    public class Tests
    {
        [Test]
        public void BasicTest()
        {
            MyStringBuilder s = "hello";
            s += " world";
            Assert.AreEqual("hello world", s.ToString());
        }
    }
}


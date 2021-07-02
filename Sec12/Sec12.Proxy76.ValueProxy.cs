using Autofac;
using Autofac.Features.Metadata;
//using JetBrains.dotMemoryUnit;
using MoreLinq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace Sec12.Proxy76
{
    public struct Percentage
    {
        [DebuggerDisplay("{value*100.0f}%")]
        private readonly float value;
        public Percentage(float value)
        {
            this.value = value;
        }
        public static float operator *(float f, Percentage p)
        {
            return f * p.value;
        }
        public static Percentage operator +(Percentage a, Percentage b)
        {
            return new Percentage(a.value + b.value);
        }
        public override string ToString()
        {
            return $"{value * 100}%";
        }
    }

    public static class PercentageExtensions
    {
        public static Percentage Percent(this float value)
        {
            return new Percentage(value / 100.0f);
        }
        public static Percentage Percent(this int value)
        {
            return new Percentage(value / 100.0f);
        }
    }
    class Demo
    {
        //static void Main(string[] args)
        //{
        //    main();
        //    ReadLine();
        //}
        static void main()
        {
            WriteLine(10f * 5.Percent());
            WriteLine(2.Percent() + 5.Percent());

        }
    }
}

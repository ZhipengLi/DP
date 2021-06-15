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

namespace Sec12.Decorator78
{
    public class MasonrySettings
    {
        public bool? All
        {
            get
            {
                if (flags.Skip(1).All(f => f == flags[0]))
                    return flags[0];
                return null;
            }

            set
            {
                if (!value.HasValue)
                    return;
                for (int i = 0; i < flags.Length; i++)
                {
                    flags[i] = value.Value;
                }
            }
        }
        //public bool? All
        //{
        //    get
        //    {
        //        if (Pillars == Walls && Walls == Floors)
        //            return Pillars;
        //        return null;
        //    }
        //    set
        //    {
        //        if (!value.HasValue)
        //            return;
        //        Pillars = value;
        //        Walls = value;
        //        Floors = value;
        //    }
        //}
        //public bool Pillars, Walls, Floors;

        private bool[] flags = new bool[3];
        public bool Pillars
        {
            get => flags[0];
            set => flags[0] = value;
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

            
        }
    }
}

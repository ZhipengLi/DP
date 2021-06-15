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
using System.Dynamic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Runtime.Remoting.Messaging;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace Sec12.Decorator77
{
    class Creature
    {
        public byte Age;
        public int X, Y;
    }

    class Creatures
    {
        private readonly int size;
        private byte[] age;
        private int[] x, y;
        public Creatures(int size)
        {
            this.size = size;
            age = new byte[size];
            x = new int[size];
            y = new int[size];
        }
        public struct CreatureProxy
        {
            private readonly Creatures creatures;
            private readonly int index;
            public CreatureProxy(Creatures creatures, int index)
            {
                this.creatures = creatures;
                this.index = index;
            }
            public ref byte Age => ref creatures.age[index];
            public ref int X => ref creatures.x[index];
            public ref int Y => ref creatures.y[index];

        }
        public IEnumerator<CreatureProxy> GetEnumerator()
        {
            for (int pos = 0; pos < size; ++pos)
            {
                yield return new CreatureProxy(this, pos);
            }
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

            // AoS (array of structures)
            //var creatures = new Creature[100];
            //foreach (var c in creatures)
            //{
            //    c.X++;
            //}

            // AoS/SoA duality
            var creatures2 = new Creatures(100);
            foreach (Creatures.CreatureProxy c in creatures2)
            {
                c.X++;
            }
        }
    }
}

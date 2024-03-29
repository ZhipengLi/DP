﻿using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using System;
using System.CodeDom;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static System.Console;

namespace Sec05.ParallelLoops35
{
    public class Program
    {
        //static void Main(string[] args)
        //{
        //    var summary = BenchmarkRunner.Run<Program>();
        //    Console.WriteLine(summary);
        //    Console.ReadKey();
        //}
        [Benchmark]
        public void SquareEachValue()
        {
            const int count = 100000;
            var values = Enumerable.Range(0, count);
            var results = new int[count];
            Parallel.ForEach(values, x => { results[x] = (int)Math.Pow(x, 2); });
            //WriteLine("Main program done.");
        }
        [Benchmark]
        public void SquareEachValueChunked()
        {
            const int count = 100000;
            var values = Enumerable.Range(0, count);
            var results = new int[count];

            var part = Partitioner.Create(0, count, 10000);
            Parallel.ForEach(part, range =>
            {
                for (int i = range.Item1; i < range.Item2; i++)
                {
                    results[i] = (int)Math.Pow(i, 2);
                }
            });
        }
    }
}



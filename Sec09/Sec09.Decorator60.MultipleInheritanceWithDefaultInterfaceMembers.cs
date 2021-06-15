using Autofac;
using Autofac.Features.Metadata;
using MoreLinq;
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

namespace Sec09.Decorator60
{
    //public interface ICreature
    //{ 
    //    int Age { get; set; }
    //}
    //public interface IBird : ICreature
    //{
    //    void Fly()
    //    {
    //        if (Age >= 10)
    //        {
    //            WriteLine("I am flying");
    //        }
    //    }
    //}
    //public interface ILizard : ICreature
    //{
    //    void Crawl()
    //    {
    //        if (Age < 10)
    //        {
    //            WriteLine("I am crawling");
    //        }
    //    }
    //}

    //public class Organism { }
    //public class Dragon : Organism, IBird, ILizard
    //{ 
    //    public int Age { get; set; }
    //}

    // inheritance
    // SmartDragon(Dragon)
    // extension methods
    // C#8 default interface methods


    //public class Demo
    //{
    //    static void Main(string[] args)
    //    {
    //        Dragon d = new Dragon { Age = 5 };
    //        if (d is IBird bird)
    //            bird.Fly();

    //        if (d is ILizard lizard)
    //            lizard.Crawl();

    //        ReadLine();
    //    }
    //}
}


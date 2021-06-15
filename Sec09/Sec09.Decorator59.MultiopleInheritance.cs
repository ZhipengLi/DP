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

namespace Sec09.Decorator59
{
    public interface IBird
    {
        int Weight { get; set; }
        void Fly();
    }
    public class Bird : IBird
    {
        public int Weight { get; set; }
        public void Fly()
        {
            WriteLine($"Soaring in the sky with weight: {Weight}");
        }
    }
    public interface ILizard
    {
        int Weight { get; set; }
        void Crawl();
    }
    public class Lizard : ILizard
    {
        public int Weight { get; set; }
        public void Crawl()
        {
            WriteLine($"Crawling in the dirt with weight: {Weight}");
        }
    }

    public class Dragon : IBird, ILizard
    {
        private int weight;
        private Bird bird = new Bird();
        private Lizard lizard = new Lizard();
        public void Crawl()
        {
            lizard.Crawl();
        }
        public void Fly()
        {
            bird.Fly();
        }

        public int Weight
        {
            get 
            {
                return weight;
            }
            set
            {
                weight = value;
                bird.Weight = value;
                lizard.Weight = value;
            }
    
        }
    }
    //public class Demo
    //{
    //    static void Main(string[] args)
    //    {
    //        var d = new Dragon();
    //        d.Weight = 123;
    //        d.Fly();
    //        d.Crawl();

    //        ReadLine();
    //    }
    //}
}


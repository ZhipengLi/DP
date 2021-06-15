using Autofac;
using Autofac.Features.Metadata;
//using JetBrains.dotMemoryUnit;
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

namespace Sec11.Decorator70
{
    public class User
    {
        private string fullName;
        public User(string fullName)
        {
            this.fullName = fullName;
        }
    }

    public class User2
    {
        static List<string> strings = new List<string>();
        private int[] names;

        public User2(string fulleName)
        { 
            int getOrAdd(string s)
            {
                int idx = strings.IndexOf(s);
                if (idx != -1)
                    return idx;
                else
                {
                    strings.Add(s);
                    return strings.Count - 1;
                }
            }

            names = fulleName.Split(' ').Select(getOrAdd).ToArray();
        }

        public string FullName => string.Join(" ",
            names.Select(i => strings[i]).ToArray());
    }

    [TestFixture]
    public class Demo
    {
        //[DotMemoryUnit(FailIfRunWithoutSupport =false)]
        [Test]
        public void TestUser()
        {
            var firstNames = Enumerable.Range(0, 100).Select(_ => RandomString());
            var lastNames = Enumerable.Range(0, 100).Select(_ => RandomString());
            var users = new List<User>();
            foreach (var firstName in firstNames)
                foreach (var lastName in lastNames)
                    users.Add(new User($"{firstName} {lastName}"));
            ForceGC();
            //dotMemory.Check(memory =>
            //{
            //    WriteLine(memory.SizeInBytes);
            //});
        }

        [Test]
        public void TestUser2()
        {
            var firstNames = Enumerable.Range(0, 100).Select(_ => RandomString());
            var lastNames = Enumerable.Range(0, 100).Select(_ => RandomString());
            var users = new List<User2>();
            foreach (var firstName in firstNames)
                foreach (var lastName in lastNames)
                    users.Add(new User2($"{firstName} {lastName}"));
            ForceGC();
        }

        private void ForceGC()
        {
            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();
        }
        private string RandomString()
        {
            Random rand = new Random();
            return new string(
                Enumerable.Range(0, 10)
                .Select(i => (char)('a' + rand.Next(26)))
                .ToArray()
                );
        }
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

using Autofac.Core.Activators;
using NUnit.Framework;
using NUnit.Framework.Internal;
using System;
using System.CodeDom;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using static System.Console;

namespace Sec26.Bonus158
{
    public static class ExtensionMethods
    {
        public struct BoolMarker<T>
        {
            public bool Result;
            public T Self;
            public enum Operation
            { 
                None,
                And,
                Or
            };
            internal Operation PendingOp;
            internal BoolMarker(bool result, T self, Operation pendingOp)
            {
                Result = result;
                Self = self;
                PendingOp = pendingOp;
            }
            public BoolMarker(bool result, T self)
                : this(result, self, Operation.None)
            { }
            public BoolMarker<T> And => new BoolMarker<T>(this.Result, Self, Operation.And);
            public static implicit operator bool(BoolMarker<T> marker)
            {
                return marker.Result;
            }
        }
        public static T AddTo<T>(this T self, params ICollection<T>[] colls)
        {
            foreach(var coll in colls)
                coll.Add(self);
            return self;
        }
        public static bool IsOneOf<T>(this T self, params T[] values)
        {
            return values.Contains(self);
        }

        public static BoolMarker<TSubject> HasNo<TSubject, T>(this TSubject self, Func<TSubject, IEnumerable<T>> props)
        {
            return new BoolMarker<TSubject>(!props(self).Any(), self);
        }

        public static BoolMarker<TSubject> HasSome<TSubject, T>(this TSubject self, Func<TSubject, IEnumerable<T>> props)
        {
            return new BoolMarker<TSubject>(props(self).Any(), self);
        }
        public static BoolMarker<T> HasNo<T, U>(this BoolMarker<T> marker, Func<T, IEnumerable<U>> props)
        {
            if (marker.PendingOp == BoolMarker<T>.Operation.And && !marker.Result)
                return marker;
            return new BoolMarker<T>(!props(marker.Self).Any(), marker.Self);
        }
    }

    public class MyClass
    {
        public void AddingNumbers()
        {
            var list = new List<int>();
            var list1 = new List<int>();
            //list.Add(24);
            24.AddTo(list, list1);
        }

        public void ProcessCommand(string opcode)
        {
            //if (opcode == "AND" || opcode == "OR" || opcode == "XOR")
            //if (new[] { "AND", "OR", "XOR" }.Contains(opcode))
            //if ("AND OR XOR".Split(' ').Contains(opcode))
            if (opcode.IsOneOf("AND", "OR", "XOR"))
            { 
            
            }
        }
        public void Process(Person person)
        {
            // if (person.Names.Count != 0)
            // if (!person.Names.Any())
            if (person.HasNo(p => p.Names).And.HasNo(p => p.Children))
            { 
            
            }
        }
    }
    //==============================================================================================
    public class Demo
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
    public class Person
    {
        public List<string> Names = new List<string>();
        public List<Person> Children = new List<Person>();
    }
    //==============================================================================================
    [TestFixture]
    public class Tests
    {
        [Test]
        public void BasicTest()
        {
            // Add to collections:
            var list = new List<int>();
            var set = new HashSet<int>();
            1.AddTo(list, set);
            Assert.IsTrue(list.Contains(1));
            Assert.IsTrue(set.Contains(1));

            // Is one of the collection:
            Assert.IsTrue(1.IsOneOf(3,4,5,1));
            Assert.IsFalse(1.IsOneOf(3, 4, 5));

            // Collection property has no content
            Person person = new Person();
            Assert.IsTrue(person.HasNo(p => p.Names));
            person.Names.Add("test");
            Assert.IsFalse(person.HasNo(p => p.Names));

            Assert.IsTrue(person.HasSome(p => p.Names));

            Assert.IsTrue(person.HasSome(p => p.Names).And.HasNo(p => p.Children));
        }
    }
}

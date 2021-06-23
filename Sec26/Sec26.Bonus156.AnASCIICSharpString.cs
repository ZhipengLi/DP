using Autofac.Core.Activators;
using NUnit.Framework;
using NUnit.Framework.Internal;
using System;
using System.CodeDom;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using static System.Console;

namespace Sec26.Bonus156
{
    // ascii utf-16
    public class str : IEquatable<str>, IEquatable<string>
    {
        protected readonly byte[] buffer;
        public str()
        {
            buffer = new byte[] { };
        }
        public str(string s)
        {
            buffer = Encoding.ASCII.GetBytes(s);
        }
        protected str(byte[] buffer)
        {
            this.buffer = buffer;
        }
        public static implicit operator str(string s)
        {
            return new str(s);
        }
        public override string ToString()
        {
            return Encoding.ASCII.GetString(buffer);
        }

        public bool Equals(str other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return ((IStructuralEquatable)buffer)
                .Equals(other.buffer,
                StructuralComparisons.StructuralEqualityComparer);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((str)obj);
        }

        public static bool operator ==(str left, str right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(str left, str right)
        {
            return !Equals(left, right);
        }

        // state space explosion here
        public static str operator +(str first, str second)
        {
            byte[] bytes = new byte[first.buffer.Length + second.buffer.Length];
            first.buffer.CopyTo(bytes, 0);
            second.buffer.CopyTo(bytes, first.buffer.Length);
            return new str(bytes);
        }
        public char this[int index]
        {
            get => (char)buffer[index];
            set => buffer[index] = (byte)value;
        }
        public override int GetHashCode()
        {
            return ToString().GetHashCode();
        }

        public bool Equals(string other)
        {
            return ToString().Equals(other);
        }
    }

    //==============================================================================================
    //public class Demo
    //{
    //    static void Main(string[] args)
    //    {
    //        main();
    //        ReadLine();
    //    }
    //    static void main()
    //    {

    //    }
    //}

    //==============================================================================================
    [TestFixture]
    public class Tests
    {
        [Test]
        public void BasicTest()
        {
            const string TestString = "test string";
            
            str str1 = new str(TestString);

            // test constructor:
            Assert.AreEqual(str1.ToString(), TestString);

            // test directly assign string value
            str str2 = TestString;
            Assert.AreEqual(str2.ToString(), TestString);
            Assert.IsFalse(ReferenceEquals(str1, str2));

            // test Equals
            Assert.IsTrue(str1.Equals(str2));
            Assert.IsTrue(str1.Equals((object)str2));

            // test ==
            Assert.IsTrue(str1 == str2);

            // test !=
            Assert.IsTrue(str1 != new str(TestString+" "));

            // test +
            Assert.IsTrue(str1 + str1 == new str(TestString + TestString));

            // test indexer
            Assert.AreEqual('e', str1[1]);
            str1[1] = 'E';
            Assert.AreEqual(str1, "tEst string");
            str1[1] = 'e';

            // compare with built-in string
            Assert.IsTrue(str1.Equals(TestString));
        }
    }
}

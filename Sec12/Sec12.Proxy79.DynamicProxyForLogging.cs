using Autofac;
using Autofac.Features.Metadata;
using ImpromptuInterface;
using MoreLinq;
using NUnit.Framework;
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
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace Sec12.Proxy79
{
    public interface IBankAccount
    {
        void Deposit(int amount);
        bool Withdraw(int amount);
        string ToString();
    }
    public class BankAccount : IBankAccount
    {
        private int balance;
        private int overdraftLimit = -500;
        public void Deposit(int amount)
        {
            balance += amount;
            WriteLine($"Deposited ${amount}, balance is now {balance}");
        }
        public bool Withdraw(int amount)
        {
            if (balance - amount >= overdraftLimit)
            {
                balance -= amount;
                WriteLine($"Withdrew ${amount}, balance is now {balance}");
                return true;
            }
            return false;
        }
        public override string ToString()
        {
            return $"{nameof(balance)}: {balance}";
        }
    }

    public class Log<T> : DynamicObject
        where T : class, new()
    {
        private readonly T subject;
        private Dictionary<string, int> methodCallCount = new Dictionary<string, int>();

        public Log(T subject)
        {
            this.subject = subject;
        }

        public static I As<I>(T subject)
            where I : class
        {
            if (!typeof(I).IsInterface)
                throw new ArgumentException("I must be an interface type!");
            return new Log<T>(subject).ActLike<I>();
        }
        public static I As<I>() where I : class
        {
            if (!typeof(I).IsInterface)
                throw new ArgumentException("I must be an interface type");

            // duck typing here!
            return new Log<T>(new T()).ActLike<I>();
        }
        public override bool TryInvokeMember(InvokeMemberBinder binder, object[] args, out object result)
        {
            try
            {
                WriteLine($"Invoking {subject.GetType().Name}.{binder.Name} with arguments [{string.Join(",", args)}]");

                if (methodCallCount.ContainsKey(binder.Name)) methodCallCount[binder.Name]++;
                else methodCallCount.Add(binder.Name, 1);

                result = subject.GetType().GetMethod(binder.Name).Invoke(subject, args);
                return true;
            }
            catch
            {
                result = null;
                return false;
            }
        }
        //public override bool TryInvokeMember(InvokeMemberBinder binder, object[] args, out object result)
        //{
        //    try
        //    {
        //        WriteLine("invoked");
        //        result = null;
        //        return true;
        //    }
        //    catch
        //    {
        //        result = null;
        //        return false;
        //    }
        //}
        public string Info
        {
            get
            {
                var sb = new StringBuilder();
                foreach (var kv in methodCallCount)
                    sb.AppendLine($"{kv.Key} called {kv.Value} time(s)");
                return sb.ToString();
            }
        }

        public override string ToString()
        {
            return $"{Info}\n{subject}";
        }
    }

    //================================================================================================
    class Demo
    {
        static void Main(string[] args)
        {
            main();
            ReadLine();
        }
        static void main()
        {
            var ba = Log<BankAccount>.As<IBankAccount>();

            ba.Deposit(100);
            ba.Withdraw(50);

            WriteLine(ba);
        }
    }

    //================================================================================================
    [TestFixture]
    public class Tests
    {
        [Test]
        public void ClassTest()
        {
            var log = new Log<BankAccount>(new BankAccount());
            Assert.IsTrue(log is DynamicObject);
            Assert.IsTrue(log.Info is string);
        }
        [Test]
        public void BasicTest()
        {
            var ba = Log<BankAccount>.As<IBankAccount>();

            ba.Deposit(100);
            ba.Withdraw(50);

            Assert.IsTrue(ba.ToString().Contains("Deposit called 1 time(s)"));
            Assert.IsTrue(ba.ToString().Contains("Withdraw called 1 time(s)"));
        }
    }
}

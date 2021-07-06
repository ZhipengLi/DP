using Autofac;
using Autofac.Core.Activators;
using ImpromptuInterface;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static System.Console;

namespace Sec19.NullObject118
{
    public interface ILog
    {
        void Info(string msg);
        void Warn(string msg);

    }
    public class Null<TInterface> : DynamicObject where TInterface : class
    { 
        public static TInterface Instance =>
               new Null<TInterface>().ActLike<TInterface>();
        public override bool TryInvokeMember(InvokeMemberBinder binder, object[] args, out object result)
        {
            result = Activator.CreateInstance(binder.ReturnType);
            return true;
        }
    }
    public class BankAccount
    {
        private ILog log;
        private int balance;
        public BankAccount(ILog log)
        {
            this.log = log;
        }
        public void Deposit(int amount)
        {
            balance += amount;
            log?.Info($"Deposited {amount}, balance is now {balance}");
        }
    }
    class Demo
    {
        static void Main(string[] args)
        {
            main();
            ReadLine();
        }
        static void main()
        {
            var log = Null<ILog>.Instance;
            log.Info("test");
            var ba = new BankAccount(log);
            ba.Deposit(100);


        }
    }

}


namespace Coding.Exercise
{
    public interface ILog
    {
        // maximum # of elements in the log
        int RecordLimit { get; }

        // number of elements already in the log
        int RecordCount { get; set; }

        // expected to increment RecordCount
        void LogInfo(string message);
    }

    public class Account
    {
        private ILog log;

        public Account(ILog log)
        {
            this.log = log;
        }

        public void SomeOperation()
        {
            int c = log.RecordCount;
            log.LogInfo("Performing an operation");
            if (c + 1 != log.RecordCount)
                throw new Exception();
            if (log.RecordCount >= log.RecordLimit)
                throw new Exception();
        }
    }

    public class NullLog : ILog
    {
        // maximum # of elements in the log
        public int RecordLimit
        {
            get
            {
                return int.MaxValue;
            }
        }

        // number of elements already in the log
        public int RecordCount
        {
            get;set;
        }

        // expected to increment RecordCount
        public void LogInfo(string message)
        { }
    }
}

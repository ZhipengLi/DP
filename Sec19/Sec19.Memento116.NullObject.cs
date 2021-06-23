using Autofac;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static System.Console;

namespace Sec19.Memento111
{
    public interface ILog
    {
        void Info(string msg);
        void Warn(string msg);

    }
    class ConsoleLog : ILog
    {
        public void Info(string msg)
        {
            WriteLine(msg);
        }
        public void Warn(string msg)
        {
            WriteLine("WARNING!!! " + msg);
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

    public class NullLog : ILog
    {
        public void Info(string msg)
        {
        }

        public void Warn(string msg)
        {
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
            var cb = new ContainerBuilder();
            cb.RegisterType<BankAccount>();
            cb.RegisterType<NullLog>().As<ILog>();
            //cb.Register(ctx => new BankAccount(null));
            using (var c = cb.Build())
            {
                var ba = c.Resolve<BankAccount>();
                ba.Deposit(100);
            }
            //var log = new ConsoleLog();
            //var ba = new BankAccount(log);
            //var ba = new BankAccount(null);

        }
    }

}

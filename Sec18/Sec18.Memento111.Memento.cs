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

namespace Sec18.Memento111
{
    public class Memento
    {
        public int Balance { get; }
        public Memento(int balance)
        {
            this.Balance = balance;
        }

    }
    public class BankAccount
    {
        private int balance;
        public BankAccount(int balance)
        {
            this.balance = balance;
        }
        public Memento Deposit(int amount)
        {
            this.balance += amount;
            return new Memento(balance);
        }

        public void Restore(Memento m)
        {
            balance = m.Balance;
        }
        public override string ToString()
        {
            return $"{nameof(balance)}: {balance}";
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
            var ba = new BankAccount(100);
            var m1 = ba.Deposit(50);
            var m2 = ba.Deposit(25);
            WriteLine(ba);

            ba.Restore(m1);
            WriteLine(ba);

            ba.Restore(m2);
            WriteLine(ba);
        }
    }

}

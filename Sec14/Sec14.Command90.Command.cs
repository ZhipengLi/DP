using Autofac;
using Autofac.Features.Metadata;
using MoreLinq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Dynamic;
using System.Linq;
using System.Net.Cache;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace Sec14.Command90
{
    public class BankAccount
    {
        private int balance;
        private int overdraftLimit = -500;
        public void Deposit(int amount)
        {
            balance += amount;
            WriteLine($"Deposited ${amount}, balance is now {balance}");
        }
        public void Withdraw(int amount)
        {
            if (balance - amount >= overdraftLimit)
            {
                balance -= amount;
                WriteLine($"withdrew ${amount}, balance is now {balance}");
            }
        }
        public override string ToString()
        {
            return $"{nameof(balance)}: {balance}";
        }
    }

    public interface ICommand
    {
        void Call();
    }

    public class BankAccountCommand : ICommand
    {
        private BankAccount account;
        public enum Action
        { 
            Deposit, Withdraw
        }
        private Action action;
        private int amount;
        public BankAccountCommand(BankAccount account, Action action, int amount)
        {
            this.account = account ?? throw new ArgumentNullException();
            this.action = action;
            this.amount = amount;
        }
        public void Call()
        {
            switch (action)
            {
                case Action.Deposit:
                    account.Deposit(amount);
                    break;
                case Action.Withdraw:
                    account.Withdraw(amount);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
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
            var ba = new BankAccount();
            var commands = new List<BankAccountCommand>()
            {
                new BankAccountCommand(ba, BankAccountCommand.Action.Deposit, 100),
                new BankAccountCommand(ba, BankAccountCommand.Action.Withdraw, 50)
            };

            WriteLine(ba);

            foreach (var c in commands)
            {
                c.Call();
            }
            WriteLine(ba);
        }
    }
}


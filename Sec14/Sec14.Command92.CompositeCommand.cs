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
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace Sec14.Command92
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
        public bool Withdraw(int amount)
        {
            if (balance - amount >= overdraftLimit)
            {
                balance -= amount;
                WriteLine($"withdrew ${amount}, balance is now {balance}");
                return true;
            }
            return false;
        }
        public override string ToString()
        {
            return $"{nameof(balance)}: {balance}";
        }
    }

    public interface ICommand
    {
        void Call();
        void Undo();
        bool Success { get; set; }
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
                    Success = true;
                    break;
                case Action.Withdraw:
                    Success = account.Withdraw(amount);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
        public void Undo()
        {
            if (!Success)
                return;
            switch (action)
            {
                case Action.Deposit:
                    account.Withdraw(amount);
                    break;
                case Action.Withdraw:
                    account.Deposit(amount);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
        public bool Success { get; set; }
    }

    public class CompositeBankAccountCommand
        : List<BankAccountCommand>, ICommand
    {
        public CompositeBankAccountCommand()
        {}
        public CompositeBankAccountCommand(IEnumerable<BankAccountCommand> collection) :base(collection)
        {}
        public virtual void Call()
        {
            ForEach(cmd => cmd.Call());
        }

        public virtual void Undo()
        {
            foreach (var cmd in ((IEnumerable<BankAccountCommand>)this).Reverse())
            {
                if (cmd.Success) cmd.Undo();
            }
        }
        public virtual bool Success 
        {
            get 
            {
                return this.All(cmd => cmd.Success);
            }
            set
            {
                foreach (var cmd in this)
                    cmd.Success = value;
            }
        }

    }
    public class MoneyTransferCommand : CompositeBankAccountCommand
    {
        public MoneyTransferCommand(BankAccount from, BankAccount to, int amount)
        {
            AddRange(new[] {
                new BankAccountCommand(from, BankAccountCommand.Action.Withdraw, amount),
                new BankAccountCommand(to, BankAccountCommand.Action.Deposit, amount)
                }
            );
        }
        public override void Call()
        {
            BankAccountCommand last = null;
            foreach (var cmd in this)
            {
                if (last == null || last.Success)
                {
                    cmd.Call();
                    last = cmd;
                }
                else
                {
                    cmd.Undo();
                    break;
                }
            }
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
            //var ba = new BankAccount();
            //var deposit = new BankAccountCommand(ba, BankAccountCommand.Action.Deposit, 100);
            //var withdraw = new BankAccountCommand(ba, BankAccountCommand.Action.Withdraw, 50);
            //var composite = new CompositeBankAccountCommand(new[] { deposit, withdraw });

            //composite.Call();
            //WriteLine(ba);

            //composite.Undo();
            //WriteLine(ba);

            var from = new BankAccount();
            from.Deposit(100);
            var to = new BankAccount();

            var mtc = new MoneyTransferCommand(from, to, 1000);
            mtc.Call();

            WriteLine(from);
            WriteLine(to);

            mtc.Undo();
            WriteLine(from);
            WriteLine(to);
        }
    }
}


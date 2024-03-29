﻿using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static System.Console;

namespace Sec02.DataSharing10
{
    public class BankAccount
    {
        public object padlock = new object();
        public int Balance { get; private set; }
        public void Deposit(int amount)
        {
            lock (padlock)
            {
                Balance += amount;
            }
        }
        public void Withdraw(int amount)
        {
            lock (padlock)
            {
                Balance -= amount;
            }
        }
    }
    class Program
    {
        //static void Main(string[] args)
        //{
        //    main();

        //    ReadLine();
        //}
        static void main()
        {
            var tasks = new List<Task>();
            var ba = new BankAccount();
            for (int i = 0; i < 10; i++)
            {
                tasks.Add(Task.Factory.StartNew(() => 
                {
                    for (int j = 0; j < 1000; j++)
                    {
                        ba.Deposit(100);
                    }
                })
                ); ;
            }
            for (int i = 0; i < 10; i++)
            {
                tasks.Add(Task.Factory.StartNew(() =>
                {
                    for (int j = 0; j < 1000; j++)
                    {
                        ba.Withdraw(100);
                    }
                })
                ); ;
            }
            Task.WaitAll(tasks.ToArray());
            Console.WriteLine($"Final balance is {ba.Balance}");
            WriteLine("Main program done.");
        }

    }
}

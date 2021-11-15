using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static System.Console;

namespace Sec02.DataSharing11
{
    public class BankAccount
    {
        private int balance;
        public object padlock = new object();
        public int Balance { get { return balance; } private set { balance = value;  } }
        public void Deposit(int amount)
        {
            Interlocked.Add(ref balance, amount);
            //    Balance += amount;

        }
        public void Withdraw(int amount)
        {

            Interlocked.Add(ref balance, -amount);

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

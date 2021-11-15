using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static System.Console;

namespace Sec02.DataSharing12
{
    public class BankAccount
    {
        private int balance;
        public object padlock = new object();
        public int Balance { get { return balance; } private set { balance = value; } }
        public void Deposit(int amount)
        {
            //Interlocked.Add(ref balance, amount);
            Balance += amount;

        }
        public void Withdraw(int amount)
        {

            //Interlocked.Add(ref balance, -amount);
            Balance -= amount;

        }
    }
    class Program
    {
        static SpinLock sl = new SpinLock(false);
        //static void Main(string[] args)
        //{
        //    main();
        //    //lockRecursion(5);
        //    ReadLine();
        //}
        static void lockRecursion(int x)
        {
            bool lockTaken = false;
            //var sl = new SpinLock(true);
            try
            {
                sl.Enter(ref lockTaken);
            }
            catch (LockRecursionException e)
            {
                Console.WriteLine("Exception: " + e);
            }
            finally
            {
                if (lockTaken)
                {
                    Console.WriteLine($"Took a lock, x= {x}");
                    lockRecursion(x - 1);
                    sl.Exit();
                }
                else
                {
                    Console.WriteLine($"Failed to take a lock, x = {x}");
                }
            }
        }
        static void main()
        {
            var tasks = new List<Task>();
            var ba = new BankAccount();

            SpinLock sl = new SpinLock(true);

            for (int i = 0; i < 10; i++)
            {
                tasks.Add(Task.Factory.StartNew(() =>
                {
                    for (int j = 0; j < 1000; j++)
                    {
                        bool lockTaken = false;
                        try
                        {
                            sl.Enter(ref lockTaken);
                            ba.Deposit(100);
                        }
                        finally
                        {
                            if (lockTaken)
                                sl.Exit();
                        }
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
                        bool lockTaken = false;
                        try
                        {
                            sl.Enter(ref lockTaken);
                            ba.Withdraw(100);
                        }
                        finally
                        {
                            if (lockTaken)
                                sl.Exit();
                        }
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

using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static System.Console;

namespace Sec02.DataSharing13
{
    public class BankAccount
    {
        private int balance;
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
        public void Transfer(BankAccount where, int amount)
        {
            Balance -= amount;
            where.Balance += amount;
        }
    }
    class Program
    {
        static void Main1(string[] args)
        {
            const string appName = "MyApp";
            Mutex mutex;

            try
            {
                mutex = Mutex.OpenExisting(appName);
                Console.WriteLine($"Sorry, {appName} is already running.");
            }
            catch (WaitHandleCannotBeOpenedException)
            {
                Console.WriteLine("We can run this program just fine.");
                mutex = new Mutex(false, appName);
            }

            Console.ReadKey();
            mutex.ReleaseMutex();
            //main();
            //ReadLine();
        }
        static void main()
        {
            var tasks = new List<Task>();
            var ba = new BankAccount();
            var ba2 = new BankAccount();

            Mutex mutex = new Mutex();
            Mutex mutex2 = new Mutex();

            for (int i = 0; i < 10; i++)
            {
                tasks.Add(Task.Factory.StartNew(() =>
                {
                    for (int j = 0; j < 1000; j++)
                    {
                        bool havelock = mutex.WaitOne();
                        try
                        {
                            ba.Deposit(1);
                        }
                        finally
                        {
                            if (havelock) mutex.ReleaseMutex();
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
                        bool havelock = mutex2.WaitOne();
                        try
                        {
                            ba2.Deposit(1);
                        }
                        finally
                        {
                            if (havelock) mutex2.ReleaseMutex();
                        }
                    }
                })
                ); ;
                tasks.Add(Task.Factory.StartNew(() => 
                {
                    for (int j = 0; j < 1000; j++)
                    {
                        bool havelock = WaitHandle.WaitAll(new[] { mutex, mutex2 });
                        try
                        {
                            ba.Transfer(ba2, 1);
                        }
                        finally
                        {
                            if (havelock)
                            {
                                mutex.ReleaseMutex();
                                mutex2.ReleaseMutex();
                            }
                        }
                    }
                }));
            }



            Task.WaitAll(tasks.ToArray());
            Console.WriteLine($"Final balance ba is {ba.Balance}");
            Console.WriteLine($"Final balance ba2 is {ba2.Balance}");

        }
    }
}


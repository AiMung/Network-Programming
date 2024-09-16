using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

namespace ThreadExample
{
    internal class Program
    {
        class MyThreadClass
        {
            private const int RANDOM_SLEEP_MAX = 1000;
            private const int LOOP_COUNT = 10;
            private String greeting;
            public MyThreadClass(String greeting)
            {
                this.greeting = greeting;
            }
            public void runMyThread()
            {
                Random rand = new Random();
                for (int x = 0; x < LOOP_COUNT; x++)
                {
                    Console.WriteLine(greeting + "(Thread ID:" + Thread.CurrentThread.GetHashCode() + ")");
                    try
                    {
                        Thread.Sleep(rand.Next(0, RANDOM_SLEEP_MAX));
                    }
                    catch (ThreadInterruptedException) { }
                }
            }

            static void Main(string[] args)
            {
                MyThreadClass mtc1 = new MyThreadClass("Day la tieu trinh thu 1"); Thread thread1 = new Thread(new ThreadStart(mtc1.runMyThread));
                thread1.Start();
                MyThreadClass mtc2 = new MyThreadClass("Day la tieu trình thu 2");
                Thread thread2 = new Thread(new ThreadStart(mtc2.runMyThread)); 
                thread2.Start();
                Console.ReadKey();
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace parallel_and_async
{
    public class MultiThreading
    {
        // Thread Abort Not supported .NET Core
        // Using Task for .NET Core 
        public void RunTest()
        {
            Console.WriteLine("--- Start Multi Threading ---");
            Thread1();
            Thread2();
            Thread3();
            Thread4("My Key");
            Thread5();
            Thread6();
        }

        public void Thread1()
        {
            Thread thread = new Thread(() =>
            {
                for (int i = 0; i < 10; i++)
                {
                    Thread.Sleep(2000);
                    Console.WriteLine("{0} - {1}", Thread.CurrentThread.Name, i);
                }
            });
            thread.Name = "Thread 1";
            thread.Start();
        }

        public void Thread2()
        {
            Thread thread = new Thread(() =>
            {
                for (int i = 0; i < 10; i++)
                {
                    Thread.Sleep(1000);
                    Console.WriteLine("{0} - {1}", Thread.CurrentThread.Name, i);
                }
            });
            thread.Name = "Thread 2";
            thread.Start();
        }

        public void Thread3()
        {
            Thread thread = new Thread(() =>
            {
                for (int i = 0; i < 10; i++)
                {
                    Thread.Sleep(1000);
                    Console.WriteLine("{0} - {1}", Thread.CurrentThread.Name, i);

                    // There is no concept of parent and child threads.
                    // One implication of this is that the child threads don't sleep when the parent thread sleeps.
                    Thread subThread = new Thread(() =>
                    {
                        Thread.Sleep(10000); // 10 + 1 Second starting write
                        Console.WriteLine("{0} - {1}", Thread.CurrentThread.Name, i);
                    });
                    subThread.Name = "SubThread";
                    subThread.Start();
                }
            });
            thread.Name = "ParentThread";
            thread.Start();
        }

        public void Thread4(string key)
        {
            Thread thread = new Thread((keyT) =>
            {
                Console.WriteLine("{0} - {1}", Thread.CurrentThread.Name, keyT);
            });
            thread.Name = "Thread 4";
            thread.Start(key);
        }

        public void Thread5()
        {
            Console.WriteLine("Hask Code 0 : " + Thread.CurrentThread.GetHashCode());
            Task task = Task.Factory.StartNew(() =>
            {
                Console.WriteLine("Hask Code 1 : " + Thread.CurrentThread.GetHashCode());
            });

            Task task2 = Task.Factory.StartNew(() =>
            {
                Console.WriteLine("Hask Code 2 : " + Thread.CurrentThread.GetHashCode());
            });
            Console.WriteLine("Hask Code 4 : " + Thread.CurrentThread.GetHashCode());

            //task.Start(); not working but task is started
            //task2.Start();
        }

        public void Thread6()
        {
            Thread thread = new Thread(()=>
            {
                Thread.Sleep(30000);
                Console.WriteLine("Background Thread is Working");
            });
            thread.IsBackground = true;
            thread.Start();
        }

    }
}

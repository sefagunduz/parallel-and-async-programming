using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace parallel_and_async
{
    public class AsyncTasking
    {
        public async Task RunAsynTest()
        {
            Console.WriteLine("--- Start Async Tasking ---");
            await Task1(); // first call first start
            await Task2(); // second call second start
            Task3().Wait(); // 3. call 3. start
            Task4().Wait(); // 4. call 4. start
            Console.WriteLine("--- Ending Async Tasking ---");
        }
        public void RunTest()
        {
            Console.WriteLine("--- Start Sync Tasking ---");
            Task3(); // working but not showing
            Task4().Wait(); // First call second start
            Console.WriteLine("--- Ending Sync Tasking ---");
            Console.ReadKey(); // showing task 3
        }

        public async Task Task1()
        {
            void InnerThread()
            {
                Thread.Sleep(1500);
                Console.WriteLine("Task 1 is started");
            }
            Task task = new Task(InnerThread);
            task.Start();
            await task;
        }

        public async Task Task2()
        {
            void InnerThread()
            {
                Thread.Sleep(500);
                Console.WriteLine("Task 2 is started");
            }
            Task task = new Task(InnerThread);
            task.Start();
            await task;
        }

        public Task Task3()
        {
            void InnerThread()
            {
                Thread.Sleep(1500);
                Console.WriteLine("Task 3 is started");
            }
            Task task = new Task(InnerThread);
            task.Start();
            return task;
        }

        public Task Task4()
        {
            void InnerThread()
            {
                Thread.Sleep(500);
                Console.WriteLine("Task 4 is started");
            }
            Task task = new Task(InnerThread);
            task.Start();
            return task;
        }

    }
}

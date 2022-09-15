using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace parallel_and_async
{
    public class MultiTasking
    {
        public void RunTest()
        {
            Console.WriteLine("--- Start Multi Tasking ---");
            Task1();
            Task2();
            Task3();
            Task4();
        }

        public void Task1()
        {
            Task task = Task.Factory.StartNew(() =>
            {
                Thread.Sleep(10000);
                Console.WriteLine("Task 1 is started");
            });
            task.Wait(); // for sleep
        }

        public void Task2()
        {
            Task<int> task = Task.Factory.StartNew<int>(() =>
            {
                return 10;
            });
            Console.WriteLine("Task 2 result : " + task.Result);
        }

        public Task Task3() // task return
        {
            return Task.Factory.StartNew(() =>
            {
                Console.WriteLine("Task 3 is started");
            });
        }

        public void Task4()
        {
            var tokenCancellationSource = new CancellationTokenSource();

            Task task = new Task(() => {
                int count = 1;
                while (true)
                {
                    tokenCancellationSource.Token.ThrowIfCancellationRequested(); // if cancel throw
                    Console.WriteLine(count++);
                }
            }, tokenCancellationSource.Token); // task parameter

            task.Start();

            Console.ReadKey();
            tokenCancellationSource.Cancel();

            try
            {
                task.Wait();
            }
            catch (AggregateException ex)
            {
                ex.Handle((exeption) =>
                {
                    if (exeption is OperationCanceledException)
                    {
                        Console.WriteLine("Task cancelled.");
                        return true;
                    }
                    return false;
                });
            }
        }
    }
}

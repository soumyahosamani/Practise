using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LangFeatures.Async
{
    public class Async
    {
        public void TestMethod()
        {
            Print("TestMethod Thread start ");
            Task task1 = new Task(() =>
            {
                Print("TestMethod Task1 body ");

            });
            task1.Start();
            Task.Run(() => Print("TestMehod Task.Run ning this "));

            task1.Wait();
            TaskCompletionSource<string> tcs = new TaskCompletionSource<string>();

            Task ContTask = new Task(() =>
            {
                Print("TaskWithContinum");
                Thread.Sleep(1000);
            });
            ContTask.ContinueWith(t => Print("Inside continum"));
            ContTask.Start();
            Print("TestMethod Thread End ");            
        }



        public Task SomeVoidMethod()
        {
            return Task.Run(
                  () =>
                  {
                      Print("SomeVoidMethod Before Sleep");
                      /// blocking
                      Thread.Sleep(1000);
                      Print("SomeVoidMethod After Sleep");
                  }
                  );
        }

        public int GetSum(int a, int b)
        {
            Thread.Sleep(1000);
            return a + b;
        }

        private void Print(string message)
        {
            Console.WriteLine("Thread {0}, Isbackground {1} , {2}", Thread.CurrentThread.ManagedThreadId, Thread.CurrentThread.IsBackground, message);
        }
    }
}

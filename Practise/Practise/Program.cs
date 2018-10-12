using LangFeatures.Async;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Practise
{
    class Program
    {
         static void Main(string[] args)
        {
            Console.WriteLine("Welcome");
            Console.WriteLine("Main Thread " + Thread.CurrentThread.ManagedThreadId + " Is Background " + Thread.CurrentThread.IsBackground);
            var async = new Async();
            /*
            async.TestMethod();
            Console.WriteLine("will this come before or after: ");
            Console.WriteLine("________________________________________________________");
            Console.WriteLine("Main Thread " + Thread.CurrentThread.ManagedThreadId + " Is Background " + Thread.CurrentThread.IsBackground);
            TestMethodAsync(async);
            Print(" Should return immediately : Async will this come before or after: ");
            */
            var sum =  TestAddAsync(async);
            Print("Main method sum " + sum.Result);
            End();
        }

        public static async Task TestMethodAsync(Async async)
        {
            Print("TestMethodAsync before await Thread");
             await async.SomeVoidMethod();
            Print("TestMethodAsync After await Thread");
        }

        public static async Task<int> TestAddAsync(Async async)
        {
            Print("TestAddAsync Before await");
            var result = await Task<int>.Run(()=> async.GetSum(10, 16));
            Print("TestAddAsync After await sum : " + result);
            return result;
        }
        

        private static void End()
        {
            Console.WriteLine("Enter any key to quit");
            Console.ReadKey();
        }

        private static void Print(string message)
        {
            Console.WriteLine("Thread {0}, Isbackground {1} , {2}", Thread.CurrentThread.ManagedThreadId, Thread.CurrentThread.IsBackground, message);
        }
    }
}

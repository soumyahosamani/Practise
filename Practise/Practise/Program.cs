using Ds_Algos.DijkstraAlgorithm;
using LangFeatures.Async;
using LangFeatures.Delegates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Practise
{
    public delegate void TestDelegate(int x);
    class Program
    {
        static void Main(string[] args)
        {
            int[,] graph =  {
                         { 0, 6, 0, 0, 0, 0, 0, 9, 0 },
                         { 6, 0, 9, 0, 0, 0, 0, 11, 0 },
                         { 0, 9, 0, 5, 0, 6, 0, 0, 2 },
                         { 0, 0, 5, 0, 9, 16, 0, 0, 0 },
                         { 0, 0, 0, 9, 0, 10, 0, 0, 0 },
                         { 0, 0, 6, 0, 10, 0, 2, 0, 0 },
                         { 0, 0, 0, 16, 0, 2, 0, 1, 6 },
                         { 9, 11, 0, 0, 0, 0, 1, 0, 5 },
                         { 0, 0, 2, 0, 0, 0, 6, 5, 0 }
                            };
            int[,]  graph1 = {
                { 1,1,1,1 },
                { 0,1,1,1 },
                { 0,1,0,1 },
                { 1,1,9,1} ,
                { 0,0,1,1} };


            Dijkstra.DijkstraAlgo(graph1, 0, 4);


            Print("Before test ");
            var t = new TestDelegate((x) =>
            {
                Print(x.ToString());

            });
            IAsyncResult re = null;
            AsyncCallback ac = (r) => { };
            t.BeginInvoke(10234324, ac, null);
            t.EndInvoke(re);
            new InvokeDelegate().InvokeDelegates();
            var c = new EventConsumer();
            c.DoWork();
            End();
            return;

            //Console.WriteLine(RemainingDays());

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
            var sum = TestAddAsync(async);
            Print("Main method sum " + sum.Result);

            try
            {
              var res = Task.Factory.StartNew<int>(() => async.ErrorMethod("mainmethodAsynInput"));
                if (res.IsFaulted)
                    Print(res.Exception.Message);
                var r = res.Result;
            }
            catch (Exception e)
            {
                Print("Mainmethod Exception " + e.Message);
            }

            try
            {
                var res = ErrorAsync(async);
                
                var r = res.Result;
            }
            catch (Exception e)
            {
                Print("Mainmethod Exception for error async " + e.Message);
            }


            End();
        }

        private static async Task<int> ErrorAsync(Async async)
        {
            int result = await  Task.Factory.StartNew<int>(() => async.ErrorMethod("mainmethodAsynInput")); 
            return result;
        }

        private static int RemainingDays()
        {
            DateTime relievingDate = new DateTime(2018, 12, 12);
            int holidays = 3;
            var v = GC.MaxGeneration;

            return (relievingDate - DateTime.Today).Days - 3;

        }

        public static async Task TestMethodAsync(Async async)
        {
            Print("TestMethodAsync before await Thread");
            await async.SomeVoidMethod();
            Print("TestMethodAsync After await Thread");
        }

        public static int TestAdd(Async async)
        {
            int result = async.GetSum(10, 16);
            Print("TestAddAsync After await sum : " + result);
            return result;
        }

        public static async Task<int> TestAddAsync(Async async)
        {
            Print("TestAddAsync before await ");
            var res = async.GetSumAsync(0, 1);
            var rr = res.ContinueWith<int>(t => t.Result);
            int result = await async.GetSumAsync(5, 5);
            Print("TestAddAsync After await ");
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

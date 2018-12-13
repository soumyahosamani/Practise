using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LangFeatures.Delegates
{
    public delegate int Operation(int a, int b);
    public delegate int Duplicate(int a, int b);

    public class MyEventArgs: EventArgs
    {
        public int EventData { get { return 9; } }
    }

    public class DelegateClass
    {
        private Class2 class2 = new Class2();
        public DelegateClass()
        {
            var newup = new Operation(Sum);
           
            Add = Sum;
            Sub = (a, b) => a-b;
            Div = new Class2().Divide;
            Mul = StaticInto;
            Pow = new Operation(GetPower);
        }

        
        public int Sum(int i, int j)
        {
            return i + j;
        }

        private static int StaticInto(int a, int b)
        {
            return a * b;
        }

        public int GetPower(int a, int b)
        {
            return a * b;
        }

        // delegates

        public Operation Add { get; private set; }
        public Operation Sub { get; private set; }
        public Operation Mul { get; private set; }
        public Operation Div { get; private set; }
        public Operation Pow { get; private set; }


    }

    public class Class2
    {
        public int Divide(int a, int b)
        {
            return a / b;
        }
    }

    public class InvokeDelegate
    {
        public void InvokeDelegates()
        {
            var delegateclass = new DelegateClass();
            int a = 10, b = 2;
            var sum = delegateclass.Add(a, b);
            var diff = delegateclass.Sub.Invoke(a, b);
            var product = delegateclass.Mul(a,b);
            var quotient = delegateclass.Div(a, b);

            Duplicate dup = delegateclass.Sum;
            
            

            // multicast
            var one = new Operation((z, y) => { Console.WriteLine("Returns First"); return z; });
            var two = new Operation((t, x) => { Console.WriteLine("Returns Second"); return x; });
            //one = two;// this is erase one and replace with two 
            one += two;// adds to invocation list of one . 

            var multi = one(a, b);//  calling one will call all added to one including one, thats how events notify to subscribers. 
            //TestFunc(one); // cant pass delegate named a to named b even if everything else is same. 
            Console.WriteLine("End of invoke");
        }

        public void TestFunc(Duplicate dup)
        {

        }
    }


    public delegate void WorkPerformedHandler(object sender, int hours);    

    public class Worker
    {
        public delegate int Cust(object s);
        public string Name { get { return "ExploreEvents"; } }
        public event WorkPerformedHandler CustomEvent;
        public event EventHandler EventHandlerEvent;
        public event EventHandler<MyEventArgs> EventHandlerEventWithData;
        public event EventHandler<MyEventArgs> MyEvent;
        public event Cust MyCust;

        public void Dowork(int hours)
        {
            
            // raising events 
            // will this throw an error?

            //if (EventHandlerEvent != null)
                EventHandlerEvent(this, EventArgs.Empty);

            // will this throw an error? yes
            CustomEvent(this,hours);
           
            //if (EventHandlerEventWithData != null)
                EventHandlerEventWithData.Invoke(this, new MyEventArgs());            
        }
    }

    

    public class EventConsumer
    {
        private Worker worker;
        public EventConsumer()
        {
            worker = new Worker();
            worker.EventHandlerEvent += OnEventHandlerEvent;
            // attaching anonymousMethod 
            worker.EventHandlerEvent += delegate (object sender, EventArgs e) {/*Do somesing*/ };
        }

        public  void DoWork()
        {
            worker.Dowork(10);
        }

        private void OnEventHandlerEvent(object sender, EventArgs e)
        {
            Console.WriteLine("Got notification from " + ((Worker)sender).Name + "with no args ");
        }
    }
}

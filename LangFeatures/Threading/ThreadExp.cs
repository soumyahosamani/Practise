using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace LangFeatures.Threading
{
  public  class ThreadExp
    {
        private object lock1;
        private int count;

        public int Counter (int seed)
        {
            Semaphore sh = new Semaphore(0, 3);
            SemaphoreSlim s = new SemaphoreSlim(0, 3);
            Mutex m = new Mutex(); m.WaitOne();m.ReleaseMutex();
            lock (lock1)
            {
                count = count + seed;
            }

            return count;
        }



    }
}

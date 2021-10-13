using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace vitual_machine_online_manager.Function
{
    class Loop
    {
        private static readonly Loop Self = new Loop();
        private Thread thread;
        private Loop() { }
        public static Loop Instance()
        {
            return Self;
        }
        public void Run(Action callback)
        {
            if (thread == null)
            {
                Self.thread = new Thread(() => LoopEvent(callback));
                thread.Start();
            }
        }
        public void Dispose(Action callback)
        {
            if (thread != null)
            {
                try
                {
                    Self.thread.Abort();
                }
                catch { }
            }
        }
        private static void LoopEvent(Action callback)
        {
            while (true)
            {
                Thread.Sleep(1000);
                callback();
            }
        }
    }
}

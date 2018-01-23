using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Threading;
using System.Diagnostics;

namespace Sylveed.Reactive
{
    public class CurrentThreadScheduler : IScheduler
    {
        [ThreadStatic]
        static Queue<Entry> s_entryQueue;

        [ThreadStatic]
        static Stopwatch s_stopwatch;

        static TimeSpan Time
        {
            get
            {
                if (s_stopwatch == null)
                    s_stopwatch = Stopwatch.StartNew();

                return s_stopwatch.Elapsed;
            }
        }

        public IDisposable Schedule(TimeSpan dueTime, Action action)
        {
            if (dueTime.Ticks > 0)
                Thread.Sleep(dueTime);

            var entry = new Entry(dueTime, action);

            if (s_entryQueue == null)
            {
                s_entryQueue = new Queue<Entry>();
                s_entryQueue.Enqueue(entry);

                try
                {
                    Consume();
                }
                finally
                {
                    s_entryQueue = null;
                }
            }
            else
            {
                s_entryQueue.Enqueue(entry);
            }

            return entry;
        }

        static void Consume()
        {
            while(s_entryQueue.Count > 0)
            {
                var entry = s_entryQueue.Dequeue();

                Thread.Sleep(entry.InvokeTime - Time);

                entry.Invoke();
            }
        }

        class Entry : IDisposable
        {
            public readonly TimeSpan InvokeTime;
            readonly Action action;

            bool isDisposed = false;

            public Entry(TimeSpan dueTime, Action action)
            {
                this.action = action;
                InvokeTime = dueTime + Time;
            }

            public void Dispose()
            {
                isDisposed = true;
            }

            public void Invoke()
            {
                if (!isDisposed)
                    action.Invoke();
            }
        }
    }
}
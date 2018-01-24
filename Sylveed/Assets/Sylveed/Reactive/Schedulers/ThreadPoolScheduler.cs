using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Threading;
using System.Diagnostics;

namespace Assets.Sylveed.Reactive
{
    public class ThreadPoolScheduler : IScheduler
    {
        public IDisposable Schedule(TimeSpan dueTime, Action action)
        {
            if (dueTime.Ticks > 0)
            {
                return ScheduleTimer(dueTime, action);
            }
            else
            {
                var d = new BooleanDisposable();

                ThreadPool.QueueUserWorkItem(_ =>
                {
                    if (!d.IsDisposed)
                        action();
                });

                return d;
            }
        }

        IDisposable ScheduleTimer(TimeSpan dueTime, Action action)
        {
            var timer = new Timer(_ =>
            {
                if (action != null)
                    action();
            }, 
            null, dueTime, Timeout.InfiniteTimeSpan);

            return Disposable.Create(() =>
            {
                action = null;
                timer.Dispose();
                timer = null;
            });
        }
    }
}
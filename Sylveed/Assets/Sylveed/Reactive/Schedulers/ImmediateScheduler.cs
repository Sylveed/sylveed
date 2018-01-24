using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Threading;

namespace Assets.Sylveed.Reactive
{
    public class ImmediateScheduler : IScheduler
    {
        public IDisposable Schedule(TimeSpan dueTime, Action action)
        {
            if (dueTime.Ticks > 0)
                Thread.Sleep(dueTime);

            action();
            return Disposable.Empty;
        }
    }
}
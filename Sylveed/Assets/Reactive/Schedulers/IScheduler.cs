using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;

namespace Sylveed.Reactive
{
    public interface IScheduler
    {
        IDisposable Schedule(TimeSpan dueTime, Action action);
    }
}
using System;
using System.Threading.Tasks;

namespace Monitors.Presentation.Helpers
{
    public static class ThreadDispatcher
    {
        public static void Dispatch(Action action)
        {
            DispatchAction(action);
        }

        public static void Dispatch<T>(Action<T> action, T arg)
        {
            DispatchAction(() => action(arg));
        }

        public static void Dispatch<T1, T2>(Action<T1, T2> action, T1 arg1, T2 arg2)
        {
            DispatchAction(() => action(arg1, arg2));
        }

        public static void Dispatch<T1, T2, T3>(Action<T1, T2, T3> action, T1 arg1, T2 arg2, T3 arg3)
        {
            DispatchAction(() => action(arg1, arg2, arg3));
        }

        static void DispatchAction(Action action)
        {
            Task.Run(action).ConfigureAwait(false);
        }
    }
}
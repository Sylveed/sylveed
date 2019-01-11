using System;
using System.Threading.Tasks;

namespace Sylveed.SampleApp.Sample.Presentation.Helpers
{
    public static class UseCaseDispatcher
    {
        public static void Dispatch<TUseCase>(Action<TUseCase> action)
        {
            //DispatchAction(() => action));
        }

        static void DispatchAction(Action action)
        {
            Task.Run(action).ConfigureAwait(false);
        }
    }
}
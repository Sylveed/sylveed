using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using Sylveed.Reactive.Operators;

namespace Sylveed.Reactive
{
    public static class Observer
    {
        public static IObserver<T> Create<T>(Action<T> onNext, Action<Exception> onError, Action onComplete)
        {
            return new AnonymousObserver<T>(onNext, onError, onComplete);
        }

        class AnonymousObserver<T> : IObserver<T>
        {
            readonly Action<T> onNext;
            readonly Action<Exception> onError;
            readonly Action onComplete;

            public AnonymousObserver(Action<T> onNext, Action<Exception> onError, Action onComplete)
            {
                this.onNext = onNext ?? Stubs<T>.Nop;
                this.onError = onError ?? Stubs.Throw;
                this.onComplete = onComplete ?? Stubs.Nop;
            }

            public void OnCompleted()
            {
                onComplete();
            }

            public void OnError(Exception error)
            {
                onError(error);
            }

            public void OnNext(T value)
            {
                onNext(value);
            }
        }
    }
}
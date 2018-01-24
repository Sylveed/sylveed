using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;

namespace Assets.Sylveed.Reactive.Operators
{
    public class ReturnOperator<T> : IObservable<T>
    {
        readonly T value;

        public ReturnOperator(T value)
        {
            this.value = value;
        }

        public IDisposable Subscribe(IObserver<T> observer)
        {
            observer.OnNext(value);
            observer.OnCompleted();
            return Disposable.Empty;
        }
    }
}
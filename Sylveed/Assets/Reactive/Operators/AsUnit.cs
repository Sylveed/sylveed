using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;

namespace Sylveed.Reactive.Operators
{
    public class AsUnitOperator<T> : IObservable<Unit>
    {
        readonly IObservable<T> source;

        public AsUnitOperator(IObservable<T> source)
        {
            this.source = source;
        }

        public IDisposable Subscribe(IObserver<Unit> observer)
        {
            return new AsUnit(observer).Run(source);
        }

        class AsUnit : IObserver<T>
        {
            readonly IObserver<Unit> observer;

            public AsUnit(IObserver<Unit> observer)
            {
                this.observer = observer;
            }

            public void OnCompleted()
            {
                observer.OnCompleted();
            }

            public void OnError(Exception error)
            {
                observer.OnError(error);
            }

            public void OnNext(T value)
            {
                observer.OnNext(Unit.Default);
            }

            public IDisposable Run(IObservable<T> source)
            {
                return source.Subscribe(this);
            }
        }
    }
}
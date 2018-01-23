using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;

namespace Sylveed.Reactive.Operators
{
    public class SelectOperator<T, TR> : IObservable<TR>
    {
        readonly IObservable<T> source;
        readonly Func<T, TR> selector;

        public SelectOperator(IObservable<T> source, Func<T, TR> selector)
        {
            this.source = source;
            this.selector = selector;
        }

        public IDisposable Subscribe(IObserver<TR> observer)
        {
            return new Select(this, observer);
        }

        class Select : IObserver<T>, IDisposable
        {
            readonly SelectOperator<T, TR> parent;
            readonly IObserver<TR> observer;
            readonly IDisposable disposable;

            public Select(SelectOperator<T, TR> parent, IObserver<TR> observer)
            {
                this.parent = parent;
                this.observer = observer;
                disposable = parent.source.Subscribe(this);
            }

            public void Dispose()
            {
                disposable.Dispose();
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
                var newValue = default(TR);

                try
                {
                    newValue = parent.selector(value);
                }
                catch(Exception ex)
                {
                    OnError(ex);
                    return;
                }

                observer.OnNext(newValue);
            }
        }
    }
}
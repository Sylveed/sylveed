using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;

namespace Assets.Sylveed.Reactive.Operators
{
    public class ConcatOperator<T> : IObservable<T>
    {
        readonly IEnumerable<IObservable<T>> sources;

        public ConcatOperator(IEnumerable<IObservable<T>> sources)
        {
            this.sources = sources.ToArray();
        }

        public IDisposable Subscribe(IObserver<T> observer)
        {
            return new Concat(this, observer);
        }

        class Concat : IObserver<T>, IDisposable
        {
            readonly IObserver<T> observer;

            IDisposable disposable = Disposable.Empty;
            
            IEnumerator<IObservable<T>> e;
            bool hasNext = false;
            
            public Concat(ConcatOperator<T> parent, IObserver<T> observer)
            {
                this.observer = observer;

                e = parent.sources.GetEnumerator();

                if (e.MoveNext())
                {
                    disposable = e.Current.Subscribe(this);
                    hasNext = e.MoveNext();
                }
            }

            public void Dispose()
            {
                disposable.Dispose();
                e.Dispose();
            }

            public void OnCompleted()
            {
                if (hasNext)
                {
                    disposable = e.Current.Subscribe(this);
                    hasNext = e.MoveNext();
                }
                else
                {
                    observer.OnCompleted();
                    Stop();
                }
            }

            public void OnError(Exception error)
            {
                observer.OnError(error);
                Stop();
            }

            public void OnNext(T value)
            {
                if (!hasNext)
                {
                    observer.OnNext(value);
                }
            }

            void Stop()
            {
                disposable.Dispose();
                e.Dispose();
            }
        }
    }
}
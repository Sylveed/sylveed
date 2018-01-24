using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;

namespace Assets.Sylveed.Reactive.Operators
{
    public class MergeOperator<T> : IObservable<T>
    {
        readonly IEnumerable<IObservable<T>> sources;

        public MergeOperator(IEnumerable<IObservable<T>> sources)
        {
            this.sources = sources.ToArray();
        }

        public IDisposable Subscribe(IObserver<T> observer)
        {
            return new Merge(observer).Run(sources);
        }

        class Merge : IObserver<T>
        {
            readonly IObserver<T> observer;

            IDisposable disposable = Disposable.Empty;

            bool isDisposed = false;
            int reaminingCount = 0;
            
            public Merge(IObserver<T> observer)
            {
                this.observer = observer;
            }

            public IDisposable Run(IEnumerable<IObservable<T>> sources)
            {
                var disposables = sources.Select(source => source.Subscribe(this)).ToArray();

                reaminingCount = disposables.Length;

                disposable = Disposable.Create(() =>
                {
                    foreach (var disposable in disposables)
                        disposable.Dispose();
                });

                return disposable;
            }

            public void OnCompleted()
            {
                ThrowIfDisposed();

                if (--reaminingCount == 0)
                {
                    try
                    {
                        observer.OnCompleted();
                    }
                    finally
                    {
                        Dispose();
                    }
                }
            }

            public void OnError(Exception error)
            {
                ThrowIfDisposed();

                try
                {
                    observer.OnError(error);
                }
                finally
                {
                    Dispose();
                }
            }

            public void OnNext(T value)
            {
                ThrowIfDisposed();

                observer.OnNext(value);
            }

            void Dispose()
            {
                if (isDisposed) return;

                isDisposed = true;
                disposable.Dispose();
            }

            void ThrowIfDisposed()
            {
                if (isDisposed) throw new ObjectDisposedException("");
            }
        }
    }
}
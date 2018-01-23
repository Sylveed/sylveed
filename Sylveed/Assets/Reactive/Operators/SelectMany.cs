using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;

namespace Sylveed.Reactive.Operators
{
    public class SelectManyOperator<T, TR> : IObservable<TR>
    {
        readonly IObservable<T> source;
        readonly Func<T, IObservable<TR>> selector;

        public SelectManyOperator(IObservable<T> source, Func<T, IObservable<TR>> selector)
        {
            this.source = source;
            this.selector = selector;
        }

        public IDisposable Subscribe(IObserver<TR> observer)
        {
            return new SelectMany(this, observer);
        }

        class SelectMany : IObserver<T>, IDisposable
        {
            readonly SelectManyOperator<T, TR> parent;
            readonly IObserver<TR> observer;
            readonly List<IDisposable> nextDisposables = new List<IDisposable>();
            readonly IDisposable disposable;
            
            public SelectMany(SelectManyOperator<T, TR> parent, IObserver<TR> observer)
            {
                this.parent = parent;
                this.observer = observer;
                disposable = parent.source.Subscribe(this);
            }

            public void Dispose()
            {
                disposable.Dispose();
                foreach (var disposable in nextDisposables)
                    disposable.Dispose();
                nextDisposables.Clear();
            }

            public void OnCompleted()
            {
            }

            public void OnError(Exception error)
            {
                observer.OnError(error);
            }

            public void OnNext(T value)
            {
                var next = default(IObservable<TR>);

                try
                {
                    next = parent.selector(value);
                }
                catch(Exception ex)
                {
                    OnError(ex);
                    return;
                }

                nextDisposables.Add(new Inner(this, next));
            }

            class Inner : IObserver<TR>, IDisposable
            {
                readonly SelectMany parent;
                readonly IDisposable disposable;

                public Inner(SelectMany parent, IObservable<TR> source)
                {
                    this.parent = parent;

                    disposable = source.Subscribe(this);
                }

                public void Dispose()
                {
                    disposable.Dispose();
                }

                public void OnCompleted()
                {
                    parent.observer.OnCompleted();
                    Stop();
                }

                public void OnError(Exception error)
                {
                    parent.observer.OnError(error);
                    Stop();
                }

                public void OnNext(TR value)
                {
                    parent.observer.OnNext(value);
                }

                void Stop()
                {
                    parent.nextDisposables.Remove(this);
                }
            }
        }
    }
}
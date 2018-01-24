using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;

namespace Assets.Sylveed.Reactive
{
    public class Subject<T> : IObservable<T>, IObserver<T>
    {
        readonly List<IObserver<T>> observers = new List<IObserver<T>>();

        public void OnCompleted()
        {
            foreach(var observer in observers)
            {
                observer.OnCompleted();
            }
        }

        public void OnError(Exception error)
        {
            foreach (var observer in observers)
            {
                observer.OnError(error);
            }
        }

        public void OnNext(T value)
        {
            foreach (var observer in observers)
            {
                observer.OnNext(value);
            }
        }

        public IDisposable Subscribe(IObserver<T> observer)
        {
            return new Subscripton(this, observer);
        }

        class Subscripton : IDisposable
        {
            readonly Subject<T> parent;
            readonly IObserver<T> observer;

            public Subscripton(Subject<T> parent, IObserver<T> observer)
            {
                this.parent = parent;
                this.observer = observer;

                parent.observers.Add(observer);
            }

            public void Dispose()
            {
                parent.observers.Remove(observer);
            }
        }
    }
}
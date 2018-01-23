using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;

namespace Sylveed.Reactive.Operators
{
    public class CreateOpeartor<T> : IObservable<T>
    {
        readonly Func<IObserver<T>, IDisposable> create;

        public CreateOpeartor(Func<IObserver<T>, IDisposable> create)
        {
            this.create = create;
        }

        public IDisposable Subscribe(IObserver<T> observer)
        {
            return create(observer);
        }
    }
}
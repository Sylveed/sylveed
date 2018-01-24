using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using Assets.Sylveed.Reactive.Operators;

namespace Assets.Sylveed.Reactive
{
    public static partial class Observable
    {
        public static IDisposable Subscribe<T>(this IObservable<T> source,
            Action<T> onNext)
        {
            return Subscribe(source, onNext, null, null);
        }

        public static IDisposable Subscribe<T>(this IObservable<T> source,
            Action onComplete)
        {
            return Subscribe(source, null, null, onComplete);
        }

        public static IDisposable Subscribe<T>(this IObservable<T> source,
            Action<T> onNext, Action onComplete)
        {
            return Subscribe(source, onNext, null, onComplete);
        }

        public static IDisposable Subscribe<T>(this IObservable<T> source,
            Action<T> onNext, Action<Exception> onError)
        {
            return Subscribe(source, onNext, onError, null);
        }

        public static IDisposable SubscribeError<T>(this IObservable<T> source,
            Action<Exception> onError)
        {
            return Subscribe(source, null, onError, null);
        }

        public static IDisposable Subscribe<T>(this IObservable<T> source,
            Action<T> onNext = null, Action<Exception> onError = null, Action onComplete = null)
        {
            return source.Subscribe(Observer.Create(onNext, onError, onComplete));
        }

        public static IObservable<T> Create<T>(Func<IObserver<T>, IDisposable> create)
        {
            return new CreateOpeartor<T>(create);
        }

        public static IObservable<T> Merge<T>(IEnumerable<IObservable<T>> sources)
        {
            return new MergeOperator<T>(sources);
        }

        public static IObservable<T> Merge<T>(params IObservable<T>[] sources)
        {
            return new MergeOperator<T>(sources);
        }
    }
}
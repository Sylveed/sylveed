using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using Assets.Sylveed.Reactive.Operators;

namespace Assets.Sylveed.Reactive
{
    public static partial class Observable
    {
        public static IObservable<Unit> AsUnitObservable<T>(this IObservable<T> source)
        {
            return new AsUnitOperator<T>(source);
        }

        public static IObservable<T> Merge<T>(this IObservable<T> source, params IObservable<T>[] others)
        {
            return new MergeOperator<T>(MergeSources(source, others));
        }

        static IEnumerable<T> MergeSources<T>(T source, IEnumerable<T> others)
        {
            yield return source;
            foreach (var e in others)
                yield return e;
        }
    }
}
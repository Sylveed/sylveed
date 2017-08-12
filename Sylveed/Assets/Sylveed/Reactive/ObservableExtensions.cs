using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UniRx;

namespace Sylveed.Reactive
{
	public static class ObservableExtensions
	{
		public static IDisposable Subscribe<T>(this IObservable<T> source, Action onComplete)
		{
			return source.Subscribe(_ => { }, onComplete);
		}

		public static IDisposable SubscribeError<T>(this IObservable<T> source, Action<Exception> onError)
		{
			return source.Subscribe(_ => { }, onError);
		}
	}
}

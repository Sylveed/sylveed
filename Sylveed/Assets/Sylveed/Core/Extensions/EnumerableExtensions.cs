using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sylveed
{
	public static class EnumerableExtensions
	{
		public static void ForEach<T>(this IEnumerable<T> source, Action<T> action)
		{
			foreach(var element in source)
			{
				action(element);
			}
		}
	}
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;

namespace Sylveed.Lang.Helpers
{
	using Expression = System.Linq.Expressions.Expression;

	public static class ConstructorCreator
	{
		public static Func<TR> Create<TR>()
		{
			return Expression.Lambda<Func<TR>>(
				Expression.New(typeof(TR).GetConstructor(null))).Compile();
		}

		public static Func<object> Create(Type type)
		{
			return Expression.Lambda<Func<object>>(
				Expression.New(type.GetConstructor(null))).Compile();
		}

		public static Func<T, TR> Create<T, TR>()
		{
			return Expression.Lambda<Func<T, TR>>(
				Expression.New(
					typeof(TR).GetConstructor(new Type[] { typeof(T) }),
					Expression.Parameter(typeof(T), "a")),
				Expression.Parameter(typeof(T), "a")).Compile();
		}

		public static Func<T, object> Create<T>(Type type)
		{
			return Expression.Lambda<Func<T, object>>(
				Expression.New(
					type.GetConstructor(new[] { typeof(T) }),
					Expression.Parameter(typeof(T), "a")),
				Expression.Parameter(typeof(T), "a")).Compile();
		}

		public static Func<object, object> Create(Type type, Type arg1)
		{
			return Expression.Lambda<Func<object, object>>(
				Expression.New(
					type.GetConstructor(new[] { arg1 }),
					Expression.Parameter(arg1, "a")),
				Expression.Parameter(arg1, "a")).Compile();
		}
	}
}

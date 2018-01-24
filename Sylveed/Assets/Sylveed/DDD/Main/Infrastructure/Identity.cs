using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Assets.Sylveed.DDD.Main.Infrastructure
{
	public class Identity<T, TValue> : IEquatable<T> where T : Identity<T, TValue>
	{
		readonly TValue value;

		public TValue Value => value;

		public Identity(TValue value)
		{
			if (Equals(value, null))
				throw new ArgumentNullException(nameof(value));

			this.value = value;
		}

		public override bool Equals(object obj)
		{
			return Equals(obj as T);
		}

		public override int GetHashCode()
		{
			return value.GetHashCode();
		}

		public bool Equals(T other)
		{
			return !Equals(other, null) && value.Equals(other.value);
		}

		public static bool operator ==(Identity<T, TValue> x, Identity<T, TValue> y)
		{
			return Equals(x, null) ? Equals(y, null) : x.Equals(y);
		}

		public static bool operator !=(Identity<T, TValue> x, Identity<T, TValue> y)
		{
			return !(x == y);
		}
	}
}

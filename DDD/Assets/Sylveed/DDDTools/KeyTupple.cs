using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Sylveed.DDDTools
{
	public struct KeyTupple : IEquatable<KeyTupple>
	{
		readonly IEnumerable<object> __identities;
		readonly int hashCode;

		IEnumerable<object> identities
		{
			get { return __identities ?? Enumerable.Empty<object>(); }
		}

		public KeyTupple(params object[] identities) : this(identities.AsEnumerable())
		{
		}

		public KeyTupple(IEnumerable<object> identities)
		{
			if (Equals(identities, null))
				throw new ArgumentNullException(nameof(identities));

			this.__identities = identities;

			hashCode = CalculateHashCode(identities);
		}

		public override bool Equals(object obj)
		{
			if (obj is KeyTupple)
				return Equals((KeyTupple)obj);
			return false;
		}

		public override int GetHashCode()
		{
			return hashCode;
		}

		public bool Equals(KeyTupple other)
		{
			using (var e1 = identities.GetEnumerator())
			using (var e2 = other.identities.GetEnumerator())
			{
				while(true)
				{
					var has1 = e1.MoveNext();
					var has2 = e2.MoveNext();
					if (has1 != has2)
						return false;
					if (!has1)
						break;

					if (!Equals(e1.Current, e2.Current))
						return false;
				}
			}
			return true;
		}

		public static bool operator ==(KeyTupple x, KeyTupple y)
		{
			return x.Equals(y);
		}

		public static bool operator !=(KeyTupple x, KeyTupple y)
		{
			return !(x == y);
		}

		static int CalculateHashCode(IEnumerable<object> identities)
		{
			var code = 0;
			foreach(var id in identities ?? Enumerable.Empty<object>())
			{
				if (!Equals(id, null))
				{
					if (code == 0)
						code = id.GetHashCode();
					else
						code = code ^ id.GetHashCode() << 2;
				}
			}
			return code;
		}
	}
}

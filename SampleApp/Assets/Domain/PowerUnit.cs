using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Sylveed.SampleApp
{
	public struct PowerUnit : IComparable<PowerUnit>, IEquatable<PowerUnit>
	{
		public static readonly PowerUnit Zero = new PowerUnit(0);

		readonly float value;

		public PowerUnit(float value)
		{
			this.value = value;
		}

		public int CompareTo(PowerUnit other)
		{
			return value.CompareTo(other.value);
		}

		public bool Equals(PowerUnit other)
		{
			return value.Equals(other.value);
		}

		public override bool Equals(object obj)
		{
			if (obj is PowerUnit)
			{
				return Equals((PowerUnit)obj);
			}

			return false;
		}

		public override int GetHashCode()
		{
			return value.GetHashCode();
		}

		public override string ToString()
		{
			return value.ToString();
		}

		public static PowerUnit operator *(PowerUnit x, float y)
		{
			return new PowerUnit(x.value * y);
		}

		public static PowerUnit operator /(PowerUnit x, float y)
		{
			return new PowerUnit(x.value / y);
		}

		public static PowerUnit operator +(PowerUnit x, PowerUnit y)
		{
			return new PowerUnit(x.value + y.value);
		}

		public static PowerUnit operator -(PowerUnit x, PowerUnit y)
		{
			return new PowerUnit(x.value - y.value);
		}

		public static PowerUnit operator +(PowerUnit x)
		{
			return new PowerUnit(+x.value);
		}

		public static PowerUnit operator -(PowerUnit x)
		{
			return new PowerUnit(-x.value);
		}


		//--- utilities

		public static PowerUnit Clamp(PowerUnit value, PowerUnit min, PowerUnit max)
		{
			return new PowerUnit(Mathf.Clamp(value.value, min.value, max.value));
		}
	}

}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace Assets.Sylveed.Lang
{
	public static class TestSource
	{
		public static string Value
		{
			get
			{
				return @"

		public static class Program
		{
			public static void Main()
			{
				var a = 0;
				var b = 1;
				var c = Add(a, b);

				Log(c);
			}

			static int Add(int a, int b)
			{
				return a + b;
			}

			static void Log(object message)
			{
				Debug.WriteLine(message);
			}
		}
";
			}
		}
		public static class Program
		{
			public static void Main()
			{
				var a = 0;
				var b = 1;
				var c = Add(a, b);

				Log(c);
			}

			static int Add(int a, int b)
			{
				return a + b;
			}

			static void Log(object message)
			{
				Debug.WriteLine(message);
			}
		}
	}
}

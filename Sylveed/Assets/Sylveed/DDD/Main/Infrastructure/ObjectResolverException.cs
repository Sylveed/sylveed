using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Assets.Sylveed.DDD.Main.Infrastructure
{
	public class ObjectResolverException : Exception
	{
		public ObjectResolverException(string message) : base(message)
		{
		}
	}
}

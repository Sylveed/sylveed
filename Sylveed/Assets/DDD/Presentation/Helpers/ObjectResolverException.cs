using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Sylveed.DDD.Presentation.Helpers
{
	public class ObjectResolverException : Exception
	{
		public ObjectResolverException(string message) : base(message)
		{
		}
	}
}

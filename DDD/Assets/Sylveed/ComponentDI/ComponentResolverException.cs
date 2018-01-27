using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Assets.Sylveed.ComponentDI
{
	public class ComponentResolverException : Exception
	{
		public ComponentResolverException(string message) : base(message)
		{
		}
	}
}

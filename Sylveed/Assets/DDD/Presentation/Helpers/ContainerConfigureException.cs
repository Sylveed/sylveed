using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Sylveed.DDD.Presentation.Helpers
{
	public class ContainerConfigureException : Exception
	{
		public ContainerConfigureException(string message) : base(message)
		{
		}
	}
}

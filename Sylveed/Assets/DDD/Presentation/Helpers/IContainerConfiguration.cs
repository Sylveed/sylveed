using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Sylveed.DDD.Presentation.Helpers
{

	public interface IContainerConfiguration
	{
		void Configure<TContainer>(TContainer container);
	}
}

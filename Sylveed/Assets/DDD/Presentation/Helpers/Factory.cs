using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Sylveed.DDD.Presentation.Helpers
{
	public class Factory<T, TParameter>
	{
		readonly Func<TParameter, T> factory;

		public Factory(Func<TParameter, T> factory)
		{
			this.factory = factory;
		}

		public T Create(TParameter parameter)
		{
			return factory(parameter);
		}
	}
}

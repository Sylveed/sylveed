using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Sylveed.DDD.Presentation.Helpers
{
	public class FactoryMapper<T>
	{
		readonly ContainerConfiguration config;
		readonly Dictionary<RuntimeTypeHandle, object> factoryMap = new Dictionary<RuntimeTypeHandle, object>();

		public FactoryMapper(ContainerConfiguration config)
		{
			this.config = config;
		}

		public FactoryMapper<T> Add<TParameter>(Func<TParameter, T> factory)
		{
			factoryMap.Add(typeof(TParameter).TypeHandle, new Factory<T, TParameter>(factory));

			return this;
		}

		public void AddTo(Dictionary<RuntimeTypeHandle, object> destination)
		{
			foreach (var kv in factoryMap)
			{
				destination.Add(kv.Key, kv.Value);
			}
		}

		public ContainerConfiguration Map()
		{
			config.AddFactoryMapper(this);

			return config;
		}
	}
}

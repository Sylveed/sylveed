using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;


namespace Sylveed.DDD.Presentation.Helpers
{
	public class ContainerConfiguration : IContainerConfiguration
	{
		readonly Dictionary<RuntimeTypeHandle, object> factoryMap = new Dictionary<RuntimeTypeHandle, object>();
		readonly Dictionary<RuntimeTypeHandle, object> repositoryMap = new Dictionary<RuntimeTypeHandle, object>();

		public FactoryMapper<T> AddFactory<T>()
		{
			return new FactoryMapper<T>(this);
		}

		public void AddFactoryMapper<T>(FactoryMapper<T> mapper)
		{
			mapper.AddTo(factoryMap);
		}

		public ContainerConfiguration AddRepository<TKey, TValue>(RepositoryBase<TKey, TValue> repository)
		{
			repositoryMap.Add(typeof(TValue).TypeHandle, repository);

			return this;
		}

		public void Configure<TContainer>(TContainer container)
		{
			var type = typeof(TContainer);

			var fields = type.GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);

			foreach (var field in fields)
			{
				var fieldType = field.FieldType;

				if (!fieldType.IsGenericType)
					continue;

				var genericTypeDefinition = fieldType.GetGenericTypeDefinition();

				if (genericTypeDefinition == typeof(RepositoryBase<,>))
				{
					var valueType = fieldType.GenericTypeArguments[1];

					object repository = null;

					try
					{
						repository = repositoryMap[valueType.TypeHandle];
					}
					catch (KeyNotFoundException)
					{
						throw new ContainerConfigureException("Repositoryが登録されていません");
					}

					field.SetValue(container, repository);
				}
				else if (genericTypeDefinition == typeof(Factory<,>))
				{
					var parameterType = fieldType.GenericTypeArguments[1];

					object factory = null;

					try
					{
						factory = factoryMap[parameterType.TypeHandle];
					}
					catch (KeyNotFoundException)
					{
						throw new ContainerConfigureException("Factoryが登録されていません");
					}

					field.SetValue(container, factory);
				}
			}
		}
	}
}

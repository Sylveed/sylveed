using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using UnityEngine;

namespace Assets.Sylveed.ComponentDI
{
	public static class ComponentResolver
	{
		static readonly Dictionary<RuntimeTypeHandle, InjectionInfo> s_injectionMap = new Dictionary<RuntimeTypeHandle, InjectionInfo>();

		static InjectionInfo GetInjectionInfo(Type targetType)
		{
			InjectionInfo info;
			if (s_injectionMap.TryGetValue(targetType.TypeHandle, out info))
				return info;

			info = CreateInjectionInfo(targetType);

			s_injectionMap.Add(targetType.TypeHandle, info);

			return info;
		}

		static class InjectionInfoCache<T>
		{
			public static InjectionInfo cache;
		}

		static InjectionInfo GetInjectionInfo<T>()
		{
			var info = InjectionInfoCache<T>.cache;

			if (info != null)
				return info;

			info = CreateInjectionInfo(typeof(T));

			InjectionInfoCache<T>.cache = info;

			return info;
		}

		static InjectionInfo CreateInjectionInfo(Type targetType)
		{
			var injectFields = targetType.GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)
				.Where(x => x.GetCustomAttribute(typeof(DIComponentAttribute)) != null)
				.ToArray();

			var injectProperties = targetType.GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)
				.Where(x => x.GetCustomAttribute(typeof(DIComponentAttribute)) != null)
				.ToArray();

			return new InjectionInfo(injectFields, injectProperties);
		}

		public static void Resolve<T>(T target) where T : Component
		{
			GetInjectionInfo<T>().Inject(target);
		}

		class InjectionInfo
		{
			readonly FieldInfo[] fields;
			readonly PropertyInfo[] properties;

			public InjectionInfo(FieldInfo[] fields, PropertyInfo[] properties)
			{
				this.fields = fields;
				this.properties = properties;
			}

			public void Inject<T>(T target) where T : Component
			{
				foreach (var x in fields)
				{
					var value = Find(target, x.FieldType);
					if (value == null)
						throw new ComponentResolverException($"{typeof(T)}'s {x.Name} not found.");

					x.SetValue(target, value);
				}

				foreach (var x in properties)
				{
					var value = Find(target, x.PropertyType);
					if (value == null)
						throw new ComponentResolverException($"{typeof(T)}'s {x.Name} not found.");

					x.SetValue(target, value);
				}
			}

			Component Find(Component target, Type type)
			{
				return target.GetComponentInChildren(type, true);
			}
		}
	}
}

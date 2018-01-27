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
			var properties = targetType.GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)
				.Select(x =>
				{
					var attrs = x.GetCustomAttributes(typeof(DIComponentAttribute), true);
					if (attrs == null || attrs.Length == 0) return null;
					var attr = attrs[0] as DIComponentAttribute;
					return new DIProperty(x, GetFindMode(attr), attr.ParentName);
				})
				.Concat(targetType.GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)
					.Select(x =>
					{
						var attrs = x.GetCustomAttributes(typeof(DIComponentAttribute), true);
						if (attrs == null || attrs.Length == 0) return null;
						var attr = attrs[0] as DIComponentAttribute;
						return new DIProperty(x, GetFindMode(attr), attr.ParentName);
					})
				)
				.Where(x => x != null)
				.ToArray();

			return new InjectionInfo(properties);
		}

		static FindMode GetFindMode(DIComponentAttribute attr)
		{
			if (attr is DITypedComponentAttribute)
				return FindMode.Type;
			if (attr is DINamedComponentAttribute)
				return FindMode.Name;

			throw new NotImplementedException();
		}

		public static void Resolve<T>(T target) where T : Component
		{
			GetInjectionInfo<T>().Inject(target);
		}

		class InjectionInfo
		{
			readonly DIProperty[] properties;

			public InjectionInfo(DIProperty[] properties)
			{
				this.properties = new DependencyResolver(properties).Resolve();
			}

			public void Inject<T>(T target) where T : Component
			{
				var cache = new Dictionary<string, IComponent>();

				var root = new SingleComponent(target);

				foreach (var x in properties)
				{
					IComponent value;
					if (!cache.TryGetValue(x.Name, out value))
					{
						value = Find(root, x, cache);
						if (value == null)
							throw new ComponentResolverException($"{typeof(T)}'s {x.Name} not found.");

						cache.Add(x.Name, value);
					}

					x.SetValue(target, value.GetValue());
				}
			}

			IComponent Find(IComponent parent, DIProperty property, Dictionary<string, IComponent> cache)
			{
				if (property.ParentName != null)
				{
					try
					{
						parent = cache[property.ParentName];
					}
					catch(KeyNotFoundException)
					{
						throw new ComponentResolverException($"parent component '{property.ParentName}' not found. ");
					}
				}

				if (property.FindMode == FindMode.Type)
				{
					return parent.FindChild(property.Type);
				}
				else if (property.FindMode == FindMode.Name)
				{
					return parent.FindChild(property.ComponentPath, property.Type);
				}
				else throw new NotImplementedException();
			}

			interface IComponent
			{
				object GetValue();
				IComponent FindChild(Type type);
				IComponent FindChild(string name, Type type);
			}

			class SingleComponent : IComponent
			{
				readonly Component value;

				public SingleComponent(Component value)
				{
					this.value = value;
				}

				public object GetValue()
				{
					return value;
				}

				public IComponent FindChild(Type type)
				{
					if (type.IsArray)
					{
						var elementType = type.GetElementType();

						return new MultipleComponent(value.GetComponentsInChildren(elementType, true), elementType);
					}
					else
					{
						return new SingleComponent(value.GetComponentInChildren(type, true));
					}
				}

				public IComponent FindChild(string name, Type type)
				{
					var transform = value.transform.Find(name);
					if (transform == null) return null;
					return new SingleComponent(transform.GetComponent(type));
				}
			}

			class MultipleComponent : IComponent
			{
				readonly IEnumerable<Component> value;
				readonly Type elementType;

				public MultipleComponent(IEnumerable<Component> value, Type elementType)
				{
					this.value = value;
					this.elementType = elementType;
				}

				public object GetValue()
				{
					var source = value.ToArray();
					var array = Array.CreateInstance(elementType, source.Length);
					for (var i = 0; i < source.Length; i++)
						array.SetValue(source[i], i);
					return array;
				}

				public IComponent FindChild(Type type)
				{
					if (type.IsArray)
					{
						var elementType = type.GetElementType();

						return new MultipleComponent(value.SelectMany(x => x.GetComponentsInChildren(elementType, true)), elementType);
					}
					else
					{
						return new MultipleComponent(value.Select(x => x.GetComponentInChildren(type, true)), type);
					}
				}

				public IComponent FindChild(string name, Type type)
				{
					return new MultipleComponent(value.Select(x =>
					{
						var transform = x.transform.Find(name);
						if (transform == null) return null;
						return transform.GetComponent(type);
					}), type);
				}
			}

			class DependencyResolver
			{
				readonly Dictionary<string, DIProperty> all;
				readonly HashSet<string> resolved = new HashSet<string>();

				int currentIndex = 0;

				public DependencyResolver(IEnumerable<DIProperty> properties)
				{
					all = properties.ToDictionary(x => x.Name);
				}

				public DIProperty[] Resolve()
				{
					var array = new DIProperty[all.Count];

					foreach (var property in all.Values)
					{
						Resolve(property, array);
					}

					return array;
				}

				void Resolve(DIProperty property, DIProperty[] array)
				{
					if (!resolved.Contains(property.Name))
					{
						DIProperty parent = null;

						if (property.ParentName != null)
						{
							if (!resolved.Contains(property.ParentName))
							{
								try
								{
									parent = all[property.ParentName];
								}
								catch (KeyNotFoundException)
								{
									throw new ComponentResolverException($"parent '{property.ParentName}' not found. ");
								}

								Resolve(parent, array);
							}
						}

						resolved.Add(property.Name);

						array[currentIndex++] = property;
					}
				}
			}
		}

		class DIProperty
		{
			readonly FieldInfo field;
			readonly PropertyInfo property;
			readonly FindMode findMode;
			readonly string parentName;

			public FindMode FindMode { get { return findMode; } }

			public Type Type
			{
				get { return property == null ? field.FieldType : property.PropertyType; }
			}

			public string Name
			{
				get { return property == null ? field.Name : property.Name; }
			}

			public string ComponentPath
			{
				get { return Name; }
			}

			public string ParentName
			{
				get { return parentName; }
			}

			public DIProperty(FieldInfo field, FindMode findMode, string parentName)
			{
				this.field = field;
				this.findMode = findMode;
				this.parentName = parentName;
			}

			public DIProperty(PropertyInfo property, FindMode findMode, string parentName)
			{
				this.property = property;
				this.findMode = findMode;
				this.parentName = parentName;
			}

			public void SetValue(object target, object value)
			{
				if (property == null)
					field.SetValue(target, value);
				else
					property.SetValue(target, value, null);
			}
		}

		enum FindMode
		{
			Type,
			Name,
		}
	}
}

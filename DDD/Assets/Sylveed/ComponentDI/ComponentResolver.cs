using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using UnityEngine;

namespace Assets.Sylveed.ComponentDI
{
	using Internal;

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
					return new DIProperty(x, attr);
				})
				.Concat(targetType.GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)
					.Select(x =>
					{
						var attrs = x.GetCustomAttributes(typeof(DIComponentAttribute), true);
						if (attrs == null || attrs.Length == 0) return null;
						var attr = attrs[0] as DIComponentAttribute;
						return new DIProperty(x, attr);
					})
				)
				.Where(x => x != null)
				.ToArray();

			return new InjectionInfo(properties);
		}

		public static void Resolve<T>(T target) where T : Component
		{
			GetInjectionInfo<T>().Inject(target);
		}

		public static void Resolve(Component target)
		{
			GetInjectionInfo(target.GetType()).Inject(target);
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

				return parent.FindChild(property);
			}

			interface IComponent
			{
				object GetValue();
				IComponent FindChild(DIProperty property);
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

				public IComponent FindChild(DIProperty property)
				{
					if (property.FindMode == FindMode.Type)
					{
						var type = property.Type;
						if (type.IsArray)
						{
							var elementType = type.GetElementType();

							return new MultipleComponent(TransformHelper.FindChildComponents(value, elementType, property.IsRecursive), elementType);
						}
						else
						{
							return new SingleComponent(TransformHelper.FindChildComponent(value, type, property.IsRecursive));
						}
					}
					else if (property.FindMode == FindMode.Name)
					{
						return new SingleComponent(TransformHelper.FindChildComponent(value, property.Type, property.ComponentPath, property.IsRecursive));
					}
					throw new NotImplementedException();
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

				public IComponent FindChild(DIProperty property)
				{
					if (property.FindMode == FindMode.Type)
					{
						var type = property.Type;
						if (type.IsArray)
						{
							var elementType = type.GetElementType();

							return new MultipleComponent(value.SelectMany(x => TransformHelper.FindChildComponents(x, elementType, property.IsRecursive)), elementType);
						}
						else
						{
							return new MultipleComponent(value.Select(x => TransformHelper.FindChildComponent(x, elementType, property.IsRecursive)), type);
						}
					}
					else if (property.FindMode == FindMode.Name)
					{
						var path = property.ComponentPath;
						var type = property.Type;

						return new MultipleComponent(value.Select(x =>
						{
							return TransformHelper.FindChildComponent(x, type, path, property.IsRecursive);
						}), type);
					}
					throw new NotImplementedException();
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
			readonly bool isRecursive = false;

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

			public bool IsRecursive
			{
				get { return isRecursive; }
			}

			public DIProperty(MemberInfo member, DIComponentAttribute attr)
			{
				if (member is PropertyInfo)
					property = member as PropertyInfo;
				else if (member is FieldInfo)
					field = member as FieldInfo;

				if (attr is DITypedComponentAttribute)
					findMode = FindMode.Type;
				else if (attr is DINamedComponentAttribute)
					findMode = FindMode.Name;

				parentName = attr.ParentName;
				isRecursive = attr.IsRecursive;
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

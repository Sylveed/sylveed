using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;


namespace Assets.Sylveed.DDDTools
{
	public class ObjectResolver
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
				.Where(x => x.GetCustomAttributes(typeof(InjectAttribute), true).Length > 0)
				.ToArray();

			var injectProperties = targetType.GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)
				.Where(x => x.GetCustomAttributes(typeof(InjectAttribute), true).Length > 0)
				.ToArray();

			var injectMethods = targetType.GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)
				.Where(x => x.GetCustomAttributes(typeof(InjectAttribute), true).Length > 0)
				.ToArray();

			return new InjectionInfo(injectFields, injectProperties, injectMethods);
		}

		readonly Dictionary<RuntimeTypeHandle, object> map = new Dictionary<RuntimeTypeHandle, object>();

        public ObjectResolver Register<T>(T @object)
        {
            map.Add(typeof(T).TypeHandle, @object);
            return this;
		}

		public ObjectResolver Register(object @object)
		{
			map.Add(@object.GetType().TypeHandle, @object);
			return this;
		}

		public ObjectResolver InheritFrom<T>(ObjectResolver parent)
		{
			return Register(parent.Resolve<T>());
		}

		public ObjectResolver DependOn(ObjectResolver dependency)
		{
			foreach (var x in map.Values)
				dependency.ResolveMembers(x, x.GetType());
			return this;
		}

		public T ResolveMembers<T>(T target, bool callMethod = true)
		{
            GetInjectionInfo<T>().Inject(this, target, callMethod);
            return target;
		}

		public object ResolveMembers(object target, Type type, bool callMethod = true)
		{
			GetInjectionInfo(type).Inject(this, target, callMethod);
			return target;
		}

		public object CallMethods(object target)
		{
			GetInjectionInfo(target.GetType()).CallMethods(target);
			return target;
		}

		public T Resolve<T>()
        {
            try
            {
                return (T)map[typeof(T).TypeHandle];
            }
            catch(KeyNotFoundException)
            {
                throw new ObjectResolverException("object not found.");
            }
		}

		public object Resolve(Type type)
		{
			try
			{
				return map[type.TypeHandle];
			}
			catch (KeyNotFoundException)
			{
				throw new ObjectResolverException("object not found.");
			}
		}

		public bool Contains(Type type)
		{
			return map.ContainsKey(type.TypeHandle);
		}

		public ObjectResolver CloneForType(Type type)
		{
			var resolver = new ObjectResolver();
			var info = GetInjectionInfo(type);

			var types = new HashSet<Type>(info.fields.Select(x => x.FieldType)
				.Concat(info.properties.Select(x => x.PropertyType)));

			foreach (var t in types)
			{
				if (Contains(t))
					resolver.Register(Resolve(t));
			}

			return resolver;
		}

        class InjectionInfo
        {
            public readonly FieldInfo[] fields;
			public readonly PropertyInfo[] properties;
			public readonly MethodInfo[] methods;

            public InjectionInfo(FieldInfo[] fields, PropertyInfo[] properties, MethodInfo[] methods)
            {
                this.fields = fields;
                this.properties = properties;
                this.methods = methods;
            }

            public void Inject(ObjectResolver parent, object target, bool callMethod)
            {
                foreach(var x in fields)
                {
					try
					{
						x.SetValue(target, parent.Resolve(x.FieldType));
					}
					catch(ObjectResolverException)
					{
						throw new ObjectResolverException(
							string.Format("object not found. \nTarget: {0}\nFieldType: {1}\nFieldName: {2}", target, x.FieldType, x.Name));
					}
                }

                foreach (var x in properties)
				{
					try
					{
						x.SetValue(target, parent.Resolve(x.PropertyType), null);
					}
					catch (ObjectResolverException)
					{
						throw new ObjectResolverException(
							string.Format("object not found.\nTarget: {0}\nPropertyType: {1}\nPropertyName: {2}", target, x.PropertyType, x.Name));
					}
                }

				if (callMethod)
				{
					CallMethods(target);
				}
            }

			public void CallMethods(object target)
			{
				foreach (var x in methods)
				{
					x.Invoke(target, null);
				}
			}
        }
	}
}

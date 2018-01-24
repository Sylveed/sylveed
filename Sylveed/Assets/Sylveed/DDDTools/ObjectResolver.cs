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
				.Where(x => x.GetCustomAttribute(typeof(InjectAttribute)) != null)
				.ToArray();

			var injectProperties = targetType.GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)
				.Where(x => x.GetCustomAttribute(typeof(InjectAttribute)) != null)
				.ToArray();

			var injectMethods = targetType.GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)
				.Where(x => x.GetCustomAttribute(typeof(InjectAttribute)) != null)
				.ToArray();

			return new InjectionInfo(injectFields, injectProperties, injectMethods);
		}

		readonly Dictionary<RuntimeTypeHandle, object> map = new Dictionary<RuntimeTypeHandle, object>();

        public ObjectResolver Register<T>(T @object)
        {
            map.Add(typeof(T).TypeHandle, @object);
            return this;
        }

		public ObjectResolver InheritFrom<T>(ObjectResolver parent)
		{
			return Register(parent.Resolve<T>());
		}

		public ObjectResolver DependOn(ObjectResolver dependency)
		{
			foreach (var x in map.Values)
				dependency.ResolveMembers(x);
			return this;
		}

		public T ResolveMembers<T>(T target)
		{
            GetInjectionInfo<T>().Inject(this, target);
            return target;
		}

		public object ResolveMembers(object target)
		{
			GetInjectionInfo(target.GetType()).Inject(this, target);
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

        class InjectionInfo
        {
            readonly FieldInfo[] fields;
            readonly PropertyInfo[] properties;
            readonly MethodInfo[] methods;

            public InjectionInfo(FieldInfo[] fields, PropertyInfo[] properties, MethodInfo[] methods)
            {
                this.fields = fields;
                this.properties = properties;
                this.methods = methods;
            }

            public void Inject<T>(ObjectResolver parent, T target)
            {
                foreach(var x in fields)
                {
                    object value;
                    if (!parent.map.TryGetValue(x.FieldType.TypeHandle, out value))
                        throw new ObjectResolverException("object not found.");

                    x.SetValue(target, value);
                }

                foreach (var x in properties)
                {
                    object value;
                    if (!parent.map.TryGetValue(x.PropertyType.TypeHandle, out value))
                        throw new ObjectResolverException("object not found.");

                    x.SetValue(target, value);
                }

                foreach (var x in methods)
                {
                    x.Invoke(target, null);
                }
            }
        }
	}
}

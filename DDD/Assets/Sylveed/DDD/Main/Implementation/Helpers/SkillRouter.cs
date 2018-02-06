using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.AI;
using Assets.Sylveed.DDD.Main.Domain.Characters;
using Assets.Sylveed.DDD.Main.Domain.Skills;
using Assets.Sylveed.ComponentDI;
using Assets.Sylveed.DDDTools;
using Assets.Sylveed.DDD.Data.Skills;
using System.Reflection;

namespace Assets.Sylveed.DDD.Main.Implementation.Helpers
{
	public static class SkillRouter
	{
		public static void Route<T>(T invoker, SkillVm skill, ISkillView view, ISkillTarget[] targets)
		{
			var method = MethodMap<T>.GetMethod(view.GetType());
			method.Invoke(invoker, skill, view, targets);
		}

		public static void Route(Type invokerType, object invoker, SkillVm skill, ISkillView view, ISkillTarget[] targets)
		{
			var method = MethodMap.GetMethod(invokerType, view.GetType());
			method.Invoke(invoker, skill, view, targets);
		}

		public static void Validate(Type invokerType, Type skillViewType)
		{
			MethodMap.GetMethod(invokerType, skillViewType);
		}

		static class MethodMap
		{
			static readonly Dictionary<RuntimeTypeHandle, Dictionary<RuntimeTypeHandle, Method>> methods =
				new Dictionary<RuntimeTypeHandle, Dictionary<RuntimeTypeHandle, Method>>();
			
			public static Method GetMethod(Type invokerType, Type key)
			{
				Dictionary<RuntimeTypeHandle, Method> map;
				if (!methods.TryGetValue(invokerType.TypeHandle, out map))
				{
					map = invokerType.GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)
						.Where(x => x.GetCustomAttributes(typeof(SkillMethodAttribute), true).Length > 0)
						.Select(x => new Method(x))
						.ToDictionary(x => x.KeyType.TypeHandle);

					methods.Add(invokerType.TypeHandle, map);
				}

				try
				{
					return map[key.TypeHandle];
				}
				catch (KeyNotFoundException)
				{
					throw new InvalidOperationException("method not found.");
				}
			}
		}

		static class MethodMap<T>
		{
			static readonly Dictionary<RuntimeTypeHandle, Method> methods;

			static MethodMap()
			{
				methods = typeof(T).GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)
					.Where(x => x.GetCustomAttributes(typeof(SkillMethodAttribute), true).Length > 0)
					.Select(x => new Method(x))
					.ToDictionary(x => x.KeyType.TypeHandle);
			}

			public static Method GetMethod(Type key)
			{
				try
				{
					return methods[key.TypeHandle];
				}
				catch (KeyNotFoundException)
				{
					throw new InvalidOperationException("method not found.");
				}
			}
		}

		class Method
		{
			static readonly HashSet<RuntimeTypeHandle> OptionalParameterTypeSet =
				new HashSet<RuntimeTypeHandle>
				{
						typeof(SkillVm).TypeHandle,
						typeof(ISkillTarget[]).TypeHandle,
				};

			readonly MethodInfo methodInfo;
			readonly Dictionary<RuntimeTypeHandle, int> parameterIndexMap;

			public Type KeyType { get; }

			public Method(MethodInfo methodInfo)
			{
				var parameters = methodInfo.GetParameters();

				Type key = null;

				foreach (var type in parameters
					.Select(x => x.ParameterType)
					.Where(x => typeof(ISkillView).IsAssignableFrom(x)))
				{
					if (key != null)
						throw new ArgumentException("multiple key types.");
					key = type;
				}

				if (key == null)
					throw new ArgumentException("key type not found.");

				KeyType = key;

				parameterIndexMap = parameters.Select((parameter, i) =>
				{
					var type = parameter.ParameterType;

					if (type != key && !OptionalParameterTypeSet.Contains(type.TypeHandle))
						throw new ArgumentException("invalid parameter type");

					return new { type, i };
				})
				.ToDictionary(x => x.type.TypeHandle, x => x.i);

				this.methodInfo = methodInfo;
			}

			public void Invoke(object target, SkillVm skill, ISkillView view, ISkillTarget[] targets)
			{
				var parameters = new object[parameterIndexMap.Count];

				int index;
				if (parameterIndexMap.TryGetValue(typeof(SkillVm).TypeHandle, out index))
					parameters[index] = skill;
				if (parameterIndexMap.TryGetValue(typeof(ISkillTarget[]).TypeHandle, out index))
					parameters[index] = targets;
				if (parameterIndexMap.TryGetValue(view.GetType().TypeHandle, out index))
					parameters[index] = view;

				methodInfo.Invoke(target, parameters);
			}
		}
	}
}

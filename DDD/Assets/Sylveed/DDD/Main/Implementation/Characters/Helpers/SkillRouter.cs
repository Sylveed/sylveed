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

namespace Assets.Sylveed.DDD.Main.Implementation.Characters.Helpers
{
    public class SkillRouter
	{
		readonly SkillVmService skillService;
		readonly ICharacterBody body;

		public SkillRouter(SkillVmService skillService, ICharacterBody body)
		{
			this.skillService = skillService;
			this.body = body;
		}

		public void Route(SkillId skillId, ISkillTarget[] targets)
		{
			var skill = skillService.Create(skillId);

			skill.Invoke(new SkillInvoker(view =>
			{
				RouteMap.Route(body, skill, view, targets);
			}));
		}

		class SkillInvoker : ISkillInvoker
		{
			readonly Action<ISkillView> invoke;

			public SkillInvoker(Action<ISkillView> invoke)
			{
				this.invoke = invoke;
			}

			public void Invoke(ISkillView skillView)
			{
				invoke(skillView);
			}
		}

		static class RouteMap
		{
			static readonly Dictionary<RuntimeTypeHandle, MethodMap> typeMap =
				new Dictionary<RuntimeTypeHandle, MethodMap>();

			public static void Route(ICharacterBody body, SkillVm skill, ISkillView view, ISkillTarget[] targets)
			{
				var methodMap = GetMethodMap(body.GetType());
				var method = methodMap.GetMethod(view.GetType());
				method.Invoke(body, skill, view, targets);
			}

			static MethodMap GetMethodMap(Type type)
			{
				MethodMap map;
				if (!typeMap.TryGetValue(type.TypeHandle, out map))
				{
					map = new MethodMap(type);
					typeMap.Add(type.TypeHandle, map);
				}

				return map;
			}

			class MethodMap
			{
				readonly Dictionary<RuntimeTypeHandle, Method> methods;

				public MethodMap(Type type)
				{
					methods = type.GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)
						.Where(x => x.GetCustomAttributes(typeof(SkillMethodAttribute), true).Length > 0)
						.Select(x => new Method(x))
						.ToDictionary(x => x.KeyType.TypeHandle);
				}

				public Method GetMethod(Type key)
				{
					try
					{
						return methods[key.TypeHandle];
					}
					catch(KeyNotFoundException)
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

				public void Invoke(ICharacterBody target, SkillVm skill, ISkillView view, ISkillTarget[] targets)
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
}

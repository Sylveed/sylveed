using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

using Assets.Sylveed.ComponentDI;
using Assets.Sylveed.ComponentDI.Internal;

namespace Assets.Sylveed.DDD.Main.Implementation.Helpers
{
    public static class ViewInjector
	{
		public static void Inject(Component target, params object[] optionals)
		{
			var targets = TransformHelper.FindChildComponentsRecursive(
				target.transform, typeof(IInjectComponent));

			foreach (var x in targets)
			{
				var sw = new System.Diagnostics.Stopwatch();
				sw.Start();
				var resolver = ServiceResolver.GetServiceResolver()
					.CloneForType(x.GetType());

				foreach (var opt in optionals)
					resolver.Register(opt);

				resolver.ResolveMembers(x, x.GetType(), false);
				Debug.Log("resolve ms: " + sw.ElapsedMilliseconds);
				ComponentResolver.Resolve(x);

				resolver.CallMethods(x);
			}
		}
	}
}

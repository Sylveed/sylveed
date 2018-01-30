using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Sylveed.ComponentDI.Internal
{
	public static class TransformHelper
	{
		public static Component FindChildComponent(Component root, Type type, bool recursive)
		{
			if (recursive)
				return FindChildComponentRecursive(root.transform, type);
			else
				return root.GetComponentInChildren(type, true);
		}

		public static IEnumerable<Component> FindChildComponents(Component root, Type type, bool recursive)
		{
			if (recursive)
				return FindChildComponentsRecursive(root.transform, type);
			else
				return root.GetComponentsInChildren(type, true);
		}

		public static Component FindChildComponent(Component root, Type type, string name, bool recursive)
		{
			if (recursive)
				return FindChildComponentRecursive(root.transform, type, name);
			else
				return FindChildComponent(root.transform, type, name);
		}

		public static IEnumerable<Component> FindChildComponents(Component root, Type type, string name, bool recursive)
		{
			if (recursive)
				return FindChildComponentsRecursive(root.transform, type, name);
			else
				return FindChildComponents(root.transform, type, name);
		}

		public static Component FindChildComponent(Transform root, Type type, string name)
		{
			var t = root.transform.Find(name);
			if (t == null)
				return null;
			return t.GetComponent(type);
		}

		public static IEnumerable<Component> FindChildComponents(Transform root, Type type, string name)
		{
			foreach (Transform t in root)
			{
				if (t.name == name)
				{
					var component = t.GetComponent(type);
					if (component != null)
						yield return component;
				}
			}
		}

		public static Component FindChildComponentRecursive(Transform root, Type type)
		{
			return FindChildComponentsRecursive(root, type).FirstOrDefault();
		}

		public static Component FindChildComponentRecursive(Transform root, Type type, string name)
		{
			return FindChildComponentsRecursive(root, type, name).FirstOrDefault();
		}

		public static IEnumerable<Component> FindChildComponentsRecursive(Transform root, Type type)
		{
			foreach (var t in CreateLevelOrderedTransformIterator(root))
			{
				var component = t.GetComponent(type);
				if (component != null)
					yield return component;
			}
		}

		public static IEnumerable<Component> FindChildComponentsRecursive(Transform root, Type type, string name)
		{
			foreach (var t in CreateLevelOrderedTransformIterator(root))
			{
				if (t.name == name)
				{
					var component = t.GetComponent(type);
					if (component != null)
						yield return component;
				}
			}
		}

		public static IEnumerable<Component> FindChildComponentsRecursive(Transform root, Type type, int limit)
		{
			var count = 0;
			foreach (var t in CreateLevelOrderedTransformIterator(root))
			{
				var component = t.GetComponent(type);
				if (component != null)
				{
					yield return component;

					if (++count >= limit)
						yield break;
				}
			}
		}

		public static IEnumerable<Transform> CreateLevelOrderedTransformIterator(Transform root)
		{
			yield return root;

			var level = 1;
			while (true)
			{
				var iterator = CreateTransformIteratorOfLevel(root, level++);
				var count = 0;
				foreach (var x in iterator)
				{
					yield return x;
					count++;
				}
				if (count == 0)
					break;
			}
		}

		static IEnumerable<Transform> CreateTransformIteratorOfLevel(Transform root, int level)
		{
			var e = Enumerable.Range(0, 1).Select(_ => root);
			while (level > 0)
			{
				e = e.SelectMany(x => x.Cast<Transform>());
				level--;
			}
			return e;
		}
	}
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Sylveed
{
	static class Sample
	{
		static void Main()
		{
			repo.Add
		}
	}

	interface IExtendableObject
	{
		T GetAddition<T>();
	}

	class Model : IExtendableObject
	{
		public Identity Identity { get; private set; }

		public T GetAddition<T>()
		{
			throw new NotImplementedException();
		}
	}

	class Repository : IExtendableObject
	{
		public T GetAddition<T>()
		{
			throw new NotImplementedException();
		}
	}

	class Service : IExtendableObject
	{
		public T GetAddition<T>()
		{
			throw new NotImplementedException();
		}
	}

	class AdditionMap
	{
		readonly Dictionary<RuntimeTypeHandle, IAddition> map =
			new Dictionary<RuntimeTypeHandle, IAddition>();

		public IEnumerable<IAddition> Values { get { return map.Values; } }

		public T Get<T>() where T : IAddition
		{
			throw new NotImplementedException();
		}

		public void Add<T>(params object[] parameters) where T : IAddition
		{
			throw new NotImplementedException();
		}
	}

	interface IAddition
	{
	}

	class ExtendableSourceAttribute : Attribute
	{

	}

	class ExtendedAdditionAttribute : Attribute
	{

	}

	class MainModel : IAddition
	{
		[ExtendableSource]
		readonly Model model;

		[ExtendedAddition]
		readonly ComponentModel component;
	}

	class ComponentModel : IAddition
	{
		[ExtendableSource]
		readonly Model model;
	}
}

using Microsoft.VisualStudio.TestTools.UnitTesting;
using Assets.Sylveed.DDD.Main.Implementation.SkillDerivations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Reflection;

namespace Assets.Sylveed.DDD.__Test.Tests
{
	[TestClass()]
	public class RefrectionPerfTest
	{
		[TestMethod()]
		public void Test()
		{
			var sw = new Stopwatch();

			var item = new Item();
			var type = item.GetType();
			var field = type.GetField("obj", BindingFlags.Instance | BindingFlags.NonPublic);
			var publicField = type.GetField("publicObj", BindingFlags.Instance | BindingFlags.Public);
			var method = type.GetMethod("Test");
			var testValue = new object();
			sw.Start();
			for(var i = 0; i < 1000000; i++)
			{
				//publicField.SetValue(item, testValue);
				//item.publicObj = testValue;
				//ctor.Invoke(null);
				method.Invoke(item, null);
				//new Item();
			}

			Debug.WriteLine(sw.ElapsedMilliseconds);
		}

		class Item
		{
			object obj;
			public object publicObj;
			int i;

			//public Item() { }
			public void Test()
			{

			}

			public void TestPublic()
			{

			}
		}
	}
}
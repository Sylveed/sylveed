using Microsoft.VisualStudio.TestTools.UnitTesting;
using Assets.Sylveed.DDD.Main.Implementation.SkillDerivations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Assets.Sylveed.DDD.__Test.Tests
{
	[TestClass()]
	public class WeakReferenceTest
	{
		[TestMethod()]
		public void Test()
		{
			Debug.WriteLine("begin");
			
			var view = new View();
			var model = new Model();
			view.Model = model;
			model.View = view;

			model = null;

			Debug.WriteLine("begin gc");
			GC.Collect();
			GC.WaitForPendingFinalizers();
			Debug.WriteLine("end gc");

			view = null;
			Debug.WriteLine("end");
		}

		class View
		{
			public Model Model { get; set; }

			~View()
			{
				Debug.WriteLine("finalize view : model = " + Model);
			}
		}

		class Model
		{
			readonly WeakReference<View> viewReference = new WeakReference<View>(null);

			public View View
			{
				get
				{
					View target;
					viewReference.TryGetTarget(out target);
					return target;
				}

				set
				{
					viewReference.SetTarget(value);
				}
			}

			~Model()
			{
				Debug.WriteLine("finalize model : view = " + View);
			}
		}
	}
}
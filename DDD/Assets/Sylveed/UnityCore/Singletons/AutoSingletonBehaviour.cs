using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Sylveed
{
	public class AutoSingletonBehaviour<T> : AutoInternalSingletonBehaviour<T>
		where T : AutoSingletonBehaviour<T>
	{
		public static new T Instance
		{
			get { return AutoInternalSingletonBehaviour<T>.Instance; }
		}
	}
}

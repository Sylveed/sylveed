using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Sylveed
{
	public class SingletonBehaviour<T> : InternalSingletonBehaviour<T>
		where T : SingletonBehaviour<T>
	{
		public static new T Instance
		{
			get { return InternalSingletonBehaviour<T>.Instance; }
		}
	}
}

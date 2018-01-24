using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Sylveed
{
	public class AutoInternalSingletonBehaviour<T> : InternalSingletonBehaviour<T>
		where T : AutoInternalSingletonBehaviour<T>
	{
		protected static new T Instance
		{
			get
			{
				if (!HasInstance)
				{
					new GameObject(typeof(T).Name).AddComponent<T>();
				}

				return InternalSingletonBehaviour<T>.Instance;
			}
		}
	}
}

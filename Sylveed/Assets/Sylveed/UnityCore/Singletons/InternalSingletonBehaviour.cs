using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Sylveed
{
	public class InternalSingletonBehaviour<T> : MonoBehaviour
		where T : InternalSingletonBehaviour<T>
	{
		static T instance;

		protected static T Instance
		{
			get
			{
				if (instance == null)
				{
					instance = FindObjectOfType<T>();

					if (instance == null)
					{
						throw new SingletonBehaviourNotFoundException(typeof(T));
					}

					instance.Awake();
				}

				return instance;
			}
		}

		public static bool HasInstance
		{
			get
			{
				if (instance != null)
					return true;

				return (instance = FindObjectOfType<T>()) != null;
			}
		}

		bool awaked = false;

		protected void Awake()
		{
			if (awaked)
				return;

			if (instance == null)
			{
				instance = (T)this;
			}
			else if (instance != this)
			{
				Destroy(gameObject);
				return;
			}

			awaked = true;

			OnAwake();
		}

		protected void OnDestroy()
		{
			if (instance == this)
				instance = null;

			OnDestroying();
		}

		protected virtual void OnAwake() { }

		protected virtual void OnDestroying() { }
	}
}

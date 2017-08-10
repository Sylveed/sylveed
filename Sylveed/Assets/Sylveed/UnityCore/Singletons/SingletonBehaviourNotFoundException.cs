using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Sylveed
{
	public class SingletonBehaviourNotFoundException : Exception
	{
		public SingletonBehaviourNotFoundException(Type type)
			: base(string.Format("シングルトン({0})が見つかりません。", type.Name))
		{

		}
	}
}

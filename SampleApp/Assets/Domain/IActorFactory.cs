using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sylveed.SampleApp
{
	public interface IActorFactory
	{
		Actor Create();
	}

}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sylveed.SampleApp
{
	public class DamageCalculateService
	{
		public DamageCalculateService()
		{
		}

		public PowerUnit Calculate(Actor attacker, Actor target, Force force)
		{
			return PowerUnit.Zero;
		}
	}

}
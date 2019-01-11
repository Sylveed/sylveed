using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sylveed.SampleApp
{
	public class CharacterAbility
	{
		public PowerUnit LifePoint { get; private set; }
		public PowerUnit SkillPoint { get; private set; }

		public CharacterAbility(
			PowerUnit lifePoint,
			PowerUnit skillPoint)
		{
			LifePoint = lifePoint;
			SkillPoint = skillPoint;
		}
	}

}
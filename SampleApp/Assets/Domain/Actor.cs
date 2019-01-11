using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sylveed.SampleApp
{
	public class Actor
	{
		readonly ActorProfile profile;

		public ActorId Id { get { return profile.Id; } }
		public ActorProfile Profile { get { return profile; } }

		public CharacterAbility Ability { get { return profile.Ability; } }

		public PowerUnit LifePoint { get; private set; }
		public PowerUnit SkillPoint { get; private set; }
		
		public Actor(ActorProfile profile)
		{
			this.profile = profile;

			RecoverFull();
		}
		
		public void AddLifePoint(PowerUnit addition)
		{
			LifePoint = PowerUnit.Clamp(LifePoint + addition, PowerUnit.Zero, Ability.LifePoint);
		}

		public void AddSkillPoint(PowerUnit addition)
		{
			SkillPoint = PowerUnit.Clamp(SkillPoint + addition, PowerUnit.Zero, Ability.SkillPoint);
		}

		void RecoverFull()
		{
			LifePoint = Ability.LifePoint;
			SkillPoint = Ability.SkillPoint;
		}
	}

}
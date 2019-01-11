using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sylveed.SampleApp
{
	public class ActorProfile
	{
		readonly SkillSet skillSet;

		public ActorId Id { get; private set; }
		public CharacterAbility Ability { get; private set; }
		public SkillSet SkillSet{ get { return skillSet; } }

		public ActorProfile(ActorId id, CharacterAbility ability, SkillSet skillSet)
		{
			Id = id;
			Ability = ability;
			this.skillSet = skillSet;
		}
	}

}
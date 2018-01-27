using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Sylveed.DDD.Data.Skills;

namespace Assets.Sylveed.DDD.Data.Characters
{
	public class CharacterSkill
	{
		public CharacterSkillId Id { get; }

		public CharacterId CharacterId => Id.CharacterId;

		public SkillId SkillId => Id.SkillId;
		
		public CharacterSkill(CharacterSkillId id)
		{
			Id = id;
		}
	}
}

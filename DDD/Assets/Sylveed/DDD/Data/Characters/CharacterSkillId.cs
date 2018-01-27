using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Sylveed.DDDTools;
using Assets.Sylveed.DDD.Data.Skills;

namespace Assets.Sylveed.DDD.Data.Characters
{
	public class CharacterSkillId : TuppleIdentity<CharacterSkillId>
	{
		public CharacterId CharacterId { get; }
		public SkillId SkillId { get; }

        public CharacterSkillId(CharacterId characterId, SkillId skillId) :
			base(characterId, skillId)
		{
			CharacterId = characterId;
			SkillId = skillId;
		}
	}
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Sylveed.DDDTools;

namespace Assets.Sylveed.DDD.Data.Characters
{
	public class CharacterSkillStorage : StorageBase<CharacterSkillId, CharacterSkill>
	{
		public IStorageIndex<CharacterId, CharacterSkill> CharacterIdIndex { get; }

		public CharacterSkillStorage() : base(x => x.Id)
		{
			CharacterIdIndex = CreateIndex(x => x.CharacterId);
		}
	}
}

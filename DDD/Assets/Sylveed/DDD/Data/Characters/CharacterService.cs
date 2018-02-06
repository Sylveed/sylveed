using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Sylveed.DDDTools;
using Assets.Sylveed.DDD.Data.Skills;

namespace Assets.Sylveed.DDD.Data.Characters
{
	public class CharacterService
	{
        [Inject]
        readonly CharacterStorage storage;
		[Inject]
		readonly CharacterSkillStorage skillStorage;

		public IEnumerable<Character> Items
		{
			get { return storage.Items; }
		}

		public Character Get(CharacterId id)
		{
			return storage.Get(id);
		}

		public IEnumerable<SkillId> GetSkillIds(CharacterId id)
		{
			if (skillStorage.CharacterIdIndex.Contains(id))
			{
				return skillStorage.CharacterIdIndex.Get(id)
					.Select(x => x.SkillId);
			}

			return Enumerable.Empty<SkillId>();
		}
	}
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Sylveed.DDD.Main.Domain.Skills;

namespace Assets.Sylveed.DDD.Main.Domain.Characters
{
	public class CharacterVmSkillSet
	{
		readonly SkillVm[] values;
		readonly Dictionary<SkillVmId, SkillVm> idValueMap;
		readonly Dictionary<CharacterVmSkillIndex, SkillVmId> indexIdMap;

		public CharacterVmSkillSet(IEnumerable<SkillVm> values)
		{
			this.values = values.ToArray();

			idValueMap = this.values.ToDictionary(x => x.Id);

			indexIdMap = this.values
				.Select((x, i) => new KeyValuePair<CharacterVmSkillIndex, SkillVmId>(new CharacterVmSkillIndex(i), x.Id))
				.ToDictionary(x => x.Key, x => x.Value);
		}

        public SkillVm GetSkill(CharacterVmSkillIndex index)
        {
            return idValueMap[indexIdMap[index]];
        }
	}
}

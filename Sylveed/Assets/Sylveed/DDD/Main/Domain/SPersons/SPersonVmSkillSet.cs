using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Sylveed.DDD.Main.Domain.Skills;

namespace Assets.Sylveed.DDD.Main.Domain.SPersons
{
	public class SPersonVmSkillSet
	{
		readonly SkillVm[] values;
		readonly Dictionary<SkillVmId, SkillVm> idValueMap;
		readonly Dictionary<SPersonVmSkillIndex, SkillVmId> indexIdMap;

		public SPersonVmSkillSet(IEnumerable<SkillVm> values)
		{
			this.values = values.ToArray();

			idValueMap = this.values.ToDictionary(x => x.Id);

			indexIdMap = this.values
				.Select((x, i) => new KeyValuePair<SPersonVmSkillIndex, SkillVmId>(new SPersonVmSkillIndex(i), x.Id))
				.ToDictionary(x => x.Key, x => x.Value);
		}

        public SkillVm GetSkill(SPersonVmSkillIndex index)
        {
            return idValueMap[indexIdMap[index]];
        }
	}
}

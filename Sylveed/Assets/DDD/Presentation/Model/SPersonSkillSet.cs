using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Sylveed.DDD.Presentation.Model
{
	public class SPersonSkillSet
	{
		readonly Skill[] values;
		readonly Dictionary<SkillId, Skill> idValueMap;
		readonly Dictionary<SPersonSkillIndex, SkillId> indexIdMap;

		public SPersonSkillSet(IEnumerable<Skill> values)
		{
			this.values = values.ToArray();

			idValueMap = this.values.ToDictionary(x => x.Id);

			indexIdMap = this.values
				.Select((x, i) => new KeyValuePair<SPersonSkillIndex, SkillId>(new SPersonSkillIndex(i), x.Id))
				.ToDictionary(x => x.Key, x => x.Value);
		}
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

namespace Sylveed.SampleApp
{
	public class SkillSet
	{
		readonly Dictionary<SkillId, Skill> skills;

		public IEnumerable<Skill> Skills { get { return skills.Values; } }

		public SkillSet(IEnumerable<Skill> skills)
		{
			this.skills = skills.ToDictionary(x => x.Id);
		}

		public Skill GetSkill(SkillId id)
		{
			return skills[id];
		}
	}

}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Sylveed.DDD.Data.Skills
{
	public class Skill
	{
		public SkillId Id { get; }
        public string Name { get; }
		public string ResourceName { get; }

		public Skill(SkillId id, string name, string resourceName)
		{
            Id = id;
            Name = name;
			ResourceName = resourceName;
		}
	}
}

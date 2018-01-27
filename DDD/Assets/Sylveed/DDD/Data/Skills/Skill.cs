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

        public Skill(SkillId id, string name)
		{
            Id = id;
            Name = name;
		}
	}
}

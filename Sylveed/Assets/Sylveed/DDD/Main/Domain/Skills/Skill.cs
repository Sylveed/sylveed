using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Assets.Sylveed.DDD.Main.Domain.Skills
{
	public class Skill
	{
		public SkillId Id { get; }

		public Skill(SkillId id)
		{
			Id = id;
		}
	}
}

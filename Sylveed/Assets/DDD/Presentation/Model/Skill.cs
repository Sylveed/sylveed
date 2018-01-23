using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Sylveed.DDD.Presentation.Model
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

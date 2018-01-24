using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Sylveed.DDDTools;

namespace Assets.Sylveed.DDD.Data.Skills
{
	public class SkillId : Identity<SkillId, int>
	{
		public SkillId(int value) : base(value)
		{
		}
	}
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Sylveed.DDDTools;


namespace Assets.Sylveed.DDD.Main.Domain.Skills
{
	public class SkillVmId : Identity<SkillVmId, int>
	{
		public SkillVmId(int value) : base(value)
		{
		}
	}
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Assets.Sylveed.DDD.Main.Domain.Skills
{
	public class SkillVm
	{
		public SkillVmId Id { get; }

		public SkillVm(SkillVmId id)
		{
			Id = id;
		}
	}
}

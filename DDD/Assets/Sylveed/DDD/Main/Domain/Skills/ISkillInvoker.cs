using Assets.Sylveed.DDD.Data.Skills;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Sylveed.DDD.Main.Domain.Skills
{
	public interface ISkillInvoker
	{
		void InvokeSkill(SkillVm skill, ISkillView skillView, ISkillTarget[] targets);
	}
}

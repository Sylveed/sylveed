using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Sylveed.DDD.Main.Domain.Skills
{
	public interface ISkillInvoker
	{
		void Invoke(ISkillView skillView);
	}
}

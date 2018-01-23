using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Sylveed.DDD.Presentation.Helpers;


namespace Sylveed.DDD.Presentation.Model
{
	public class SkillId : Identity<SkillId, int>
	{
		public SkillId(int value) : base(value)
		{
		}
	}
}

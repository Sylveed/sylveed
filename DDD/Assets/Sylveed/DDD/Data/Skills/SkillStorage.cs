using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Sylveed.DDDTools;

namespace Assets.Sylveed.DDD.Data.Skills
{
	public class SkillStorage : StorageBase<SkillId, Skill>
	{
		public SkillStorage() : base(x => x.Id)
		{

		}
	}
}

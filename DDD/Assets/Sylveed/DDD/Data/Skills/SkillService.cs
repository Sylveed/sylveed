using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Sylveed.DDDTools;


namespace Assets.Sylveed.DDD.Data.Skills
{
	public class SkillService
	{
        [Inject]
        readonly SkillStorage storage;

		public IEnumerable<Skill> Items
		{
			get { return storage.Items; }
		}
	}
}

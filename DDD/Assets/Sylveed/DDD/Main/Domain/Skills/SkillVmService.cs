using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Sylveed.DDDTools;
using Assets.Sylveed.DDD.Data.Skills;
using Assets.Sylveed.DDD.Main.Domain.Characters;

namespace Assets.Sylveed.DDD.Main.Domain.Skills
{
	public class SkillVmService
	{
        [Inject]
		readonly ISkillVmFactory factory;
        [Inject]
        readonly SkillVmStorage storage;

		public SkillVm Create(SkillId skillId)
		{
			return factory.Create(skillId, new SkillVmId());
		}

		public void Trim()
		{
			foreach (var e in storage.Items.Where(x => x.IsDisposed).ToArray())
			{
				storage.Remove(e.Id);
			}
		}
	}
}

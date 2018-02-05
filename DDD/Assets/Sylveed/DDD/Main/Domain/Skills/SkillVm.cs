using Assets.Sylveed.DDD.Data.Skills;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Sylveed.DDD.Main.Domain.Skills
{
	public class SkillVm
	{
		readonly ISkillView view;

		public SkillVmId Id { get; }
		public Skill Skill { get; }

		public bool IsDisposed
		{
			get { return view == null; }
		}

		public SkillVm(SkillVmId id, Skill skill, ISkillView view)
		{
			Id = id;
			Skill = skill;
			this.view = view;
		}

		public void Invoke(ISkillInvoker invoker, ISkillTarget[] targets)
		{
			invoker.InvokeSkill(this, view, targets);
		}
	}
}

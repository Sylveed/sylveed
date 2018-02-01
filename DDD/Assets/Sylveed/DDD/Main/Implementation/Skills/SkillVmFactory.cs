using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UniRx;

using Assets.Sylveed.DDDTools;
using Assets.Sylveed.DDD.Main.Domain.Skills;
using Assets.Sylveed.DDD.Data.Skills;
using Assets.Sylveed.DDD.Main.Domain.Characters;
using Assets.Sylveed.DDD.Main.Application;
using UnityEngine;

namespace Assets.Sylveed.DDD.Main.Implementation.Skills
{
    public class SkillVmFactory : ISkillVmFactory
    {
		[Inject]
		readonly SkillService skillService;
		[Inject]
		readonly ResourceProvider resourceProvider;

		public SkillVm Create(SkillId skillId, SkillVmId id)
		{
			var prefab = resourceProvider.ResourceSet.Skills.ShootBulletView;

			var skill = skillService.Get(skillId);
			var view = GameObject.Instantiate(prefab);

			return new SkillVm(id, skill, view);
		}
    }
}
